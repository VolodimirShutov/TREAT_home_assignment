using System.Collections;
using System.Collections.Generic;
using Features.Gameplay.Cards;
using Packages.GameControl.Signals;
using ShootCommon.SignalSystem;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Packages.GameControl.Card
{
    public class CardManagerPresenter : MonoBehaviour
    {
        [Inject] private GameController _gameController;
        [Inject] private SignalService _signalService;
        [SerializeField] private CardsConfig cardsConfig;

        [SerializeField] private Transform gridParent;
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private GameObject sreenBlocker;

        private List<CardPresenter> _activeCards = new List<CardPresenter>();

        private CardPresenter _firstFlipped;
        private CardPresenter _secondFlipped;
        
        private void Awake()
        {
            ProjectContext.Instance.Container.Inject(this);
            _gameController.Try = 0;
            GenerateCards();
            sreenBlocker.SetActive(false);
        }

        private void GenerateCards()
        {
            int pairsNeeded = _gameController.GameModel.PairsCount;
            ClearPreviousCards();

            List<CardEntryModel> cardDeck = CreateShuffledDeck(pairsNeeded);
            SpawnCardsFromDeck(cardDeck);
            AdjustGridLayout(pairsNeeded);
        }

        private void ClearPreviousCards()
        {
            foreach (Transform child in gridParent)
            {
                Destroy(child.gameObject);
            }

            _activeCards.Clear();
            _firstFlipped = null;
            _secondFlipped = null;
        }

        private List<CardEntryModel> CreateShuffledDeck(int pairsNeeded)
        {
            int modelsToTake = Mathf.Min(pairsNeeded, cardsConfig.cards.Count);

            List<CardEntryModel> selectedModels = new List<CardEntryModel>();
            for (int i = 0; i < modelsToTake; i++)
            {
                selectedModels.Add(cardsConfig.cards[i]);
            }
            List<CardEntryModel> cardDeck = new List<CardEntryModel>();
            foreach (var model in selectedModels)
            {
                cardDeck.Add(model);
                cardDeck.Add(model);
            }

            Shuffle(cardDeck);
            return cardDeck;
        }

        private void SpawnCardsFromDeck(List<CardEntryModel> cardDeck)
        {
            foreach (var model in cardDeck)
            {
                GameObject cardObj = Instantiate(cardPrefab, gridParent);
                CardPresenter card = cardObj.GetComponent<CardPresenter>();

                card.SetFrontSprite(model.frontSprite);
                card.SetBackSprite(cardsConfig.backSprite);
                card.CardId = model.cardId; 

                card.onFlipped.AddListener(() => OnCardFlipped(card));
                card.onFlippedBack.AddListener(() => OnCardFlippedBack(card));

                _activeCards.Add(card);
            }
        }

        private void AdjustGridLayout(int pairsNeeded)
        {
            var grid = gridParent.GetComponent<GridLayoutGroup>();
            if (grid != null)
            {
                int totalCards = pairsNeeded * 2;
                int columns = Mathf.CeilToInt(Mathf.Sqrt(totalCards));
                grid.constraintCount = columns;
            }
        }

        private void OnCardFlipped(CardPresenter card)
        {
            if (_firstFlipped == null)
            {
                _firstFlipped = card;
            }
            else if (_secondFlipped == null && card != _firstFlipped)
            {
                _secondFlipped = card;

                if (_firstFlipped.CardId == _secondFlipped.CardId)
                {
                    _firstFlipped.PlayWinEffect();
                    _secondFlipped.PlayWinEffect();
                    
                    _firstFlipped = null;
                    _secondFlipped = null;

                    CheckWinCondition();
                }
                else
                {
                    sreenBlocker.SetActive(true);
                    StartCoroutine(DelayedFlipBack(0.8f));
                }
            }
        }

        private void OnCardFlippedBack(CardPresenter card)
        {
            sreenBlocker.SetActive(false);
        }

        private IEnumerator DelayedFlipBack(float delay)
        {
            yield return new WaitForSeconds(delay);

            _gameController.Try++;
            _signalService.Send(new NewTrySignal());

            _firstFlipped?.FlipToBack();
            _secondFlipped?.FlipToBack();

            _firstFlipped = null;
            _secondFlipped = null;
        }

        private void CheckWinCondition()
        {
            bool allFlipped = _activeCards.TrueForAll(card => card.IsFlipped);

            if (allFlipped)
            {   
                _signalService.Send(new GameWinSignal());
            }
        }

        private void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}
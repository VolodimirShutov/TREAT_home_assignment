using System.Collections.Generic;
using UnityEngine;

namespace Packages.GameControl.Card
{

    [CreateAssetMenu(fileName = "CardsConfig", menuName = "TREAT/Game/Cards Config", order = 1)]
    public class CardsConfig : ScriptableObject
    {

        [Header("")] [Tooltip("Set card front sprite")]
        public List<CardEntryModel> cards = new List<CardEntryModel>();

        [Header("optional")]
        public Sprite backSprite;

        public Sprite GetFrontSprite(string cardId)
        {
            var entry = cards.Find(c => c.cardId == cardId);
            return entry != null ? entry.frontSprite : null;
        }

        public int GetCardCount() => cards.Count;

        public bool HasCard(string cardId) => cards.Exists(c => c.cardId == cardId);
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

namespace Features.Gameplay.Cards
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CardPresenter : MonoBehaviour
    {
        [Header("Visuals")]
        [SerializeField] private Image cardBack;   
        [SerializeField] private Image cardFront;

        [Header("Particles (optional using)")]
        [SerializeField] private ParticleSystem winParticles;

        [Header("Animation Settings")]
        [SerializeField] private float flipDuration = 0.4f;

        [Header("Events")]
        public UnityEvent onFlipped = new UnityEvent();       
        public UnityEvent onFlippedBack = new UnityEvent();  

        private bool _isFlipped = false;
        private bool _isAnimating = false;
        private string _cardId = "";

        public string CardId
        {
            get => _cardId;
            set => _cardId = value;
        }
        
        public bool IsFlipped => _isFlipped;
        
        private void Awake()
        {
            ResetToBackInstant();
        }

        public void SetFrontSprite(Sprite sprite)
        {
            cardFront.sprite = sprite;
        }
        
        // Thinking about castomization
        public void SetBackSprite(Sprite sprite)
        {
            cardBack.sprite = sprite;
        }

        public void OnCardClicked()
        {
            Debug.Log("OnCardClicked");
            if (_isAnimating || _isFlipped) return;

            FlipToFront();
        }

        private void FlipToFront()
        {
            if (_isAnimating) return;
            _isAnimating = true;
            _isFlipped = true;

            StartCoroutine(FlipAnimation(180f, () =>
            {
                _isAnimating = false;
                onFlipped?.Invoke(); 
            }));
        }

        public void FlipToBack()
        {
            if (_isAnimating) return;
            _isAnimating = true;
            _isFlipped = false;

            StartCoroutine(FlipAnimation(0f, () =>
            {
                _isAnimating = false;
                onFlippedBack?.Invoke(); 
            }));
        }

        private IEnumerator FlipAnimation(float targetYAngle, System.Action onComplete)
        {
            Quaternion startRot = transform.localRotation;
            Quaternion endRot = Quaternion.Euler(0, targetYAngle, 0);

            float elapsed = 0f;

            while (elapsed < flipDuration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / flipDuration;

                transform.localRotation = Quaternion.Slerp(startRot, endRot, t);

                float angle = Mathf.Abs(transform.localEulerAngles.y);
                cardBack.enabled = angle < 90f || angle > 270f;
                cardFront.enabled = angle >= 90f && angle <= 270f;

                yield return null;
            }
            
            transform.localRotation = endRot;
            cardBack.enabled = targetYAngle == 0f;
            cardFront.enabled = targetYAngle == 180f;

            onComplete?.Invoke();
        }

        public void PlayWinEffect()
        {
            StartCoroutine(ShakeAnimation());

            if (winParticles != null)
            {
                winParticles.Play();
            }
        }

        private IEnumerator ShakeAnimation()
        {
            Vector3 originalPos = transform.localPosition;
            float elapsed = 0f;
            float duration = 0.5f;
            float strength = 10f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                float shake = Mathf.Sin(t * Mathf.PI * 10) * strength * (1f - t);
                transform.localPosition = originalPos + new Vector3(shake, shake * 0.5f, 0);

                yield return null;
            }

            transform.localPosition = originalPos;
        }

        public void ResetToBackInstant()
        {
            transform.localRotation = Quaternion.identity;
            cardBack.enabled = true;
            cardFront.enabled = false;
            _isFlipped = false;
            _isAnimating = false;
        }
    }
}
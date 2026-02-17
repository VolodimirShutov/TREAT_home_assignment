using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using ShootCommon.SignalSystem;
using UnityEngine;
using Zenject;

namespace FirebaseModul
{
    public class FirebaseController : IInitializable
    {
        private FirebaseApp _app;
        private FirebaseAuth _auth;
        private FirebaseFirestore _firestore;

        public bool IsInitialized { get; private set; }
        public string UserId { get; private set; } // UID або анонімний ідентифікатор

        [Inject] private SignalService _signalService; // якщо хочеш сигнал після ініціалізації

        public async void Initialize()
        {
            try
            {
                var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync();
                if (dependencyStatus == DependencyStatus.Available)
                {
                    _app = FirebaseApp.DefaultInstance;
                    _auth = FirebaseAuth.DefaultInstance;
                    _firestore = FirebaseFirestore.DefaultInstance;

                    // Анонімна автентифікація (рекомендую для тестової задачі — швидко і без форми логіну)
                    await SignInAnonymouslyAsync();

                    IsInitialized = true;
                    Debug.Log("Firebase initialized successfully. User ID: " + UserId);

                    // Можна відправити сигнал
                    // _signalService.Send(new FirebaseReadySignal());
                }
                else
                {
                    Debug.LogError("Firebase dependencies failed: " + dependencyStatus);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Firebase init error: " + ex.Message);
            }
        }

        private async Task SignInAnonymouslyAsync()
        {
            var authResult = await _auth.SignInAnonymouslyAsync();
            UserId = authResult.User.UserId;
            Debug.Log("Anonymous sign-in successful: " + UserId);
        }

        // Збереження результату гри
        public async Task SaveGameResultAsync(string playerName, int difficultyLevel, int moves, int score = 0)
        {
            if (!IsInitialized || string.IsNullOrEmpty(UserId))
            {
                Debug.LogWarning("Firebase not ready or no user");
                return;
            }

            var data = new
            {
                playerName = playerName,
                difficulty = difficultyLevel,
                moves = moves,
                score = score,
                timestamp = FieldValue.ServerTimestamp,
                sessionDate = DateTime.UtcNow.ToString("o")
            };

            // Зберігаємо в колекцію sessions (або results)
            var docRef = _firestore.Collection("players").Document(UserId).Collection("sessions").Document();
            await docRef.SetAsync(data);

            // Опціонально: оновити high-score (bonus)
            var playerDoc = _firestore.Collection("players").Document(UserId);
            await playerDoc.SetAsync(new {bestMoves = moves, bestDifficulty = difficultyLevel}, SetOptions.MergeAll);

            Debug.Log($"Game result saved for {playerName} | Moves: {moves}");
        }

    }
}
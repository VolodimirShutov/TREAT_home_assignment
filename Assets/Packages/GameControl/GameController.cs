using Packages.GameControl.GameSignals;
using Packages.GameControl.Signals;
using ShootCommon.SignalSystem;
using UnityEngine;
using Zenject;

namespace Packages.GameControl
{
    public class GameController
    {
        private SignalService _signalService;
        
        private GameModel _gameModel = new GameModel();
        
        public GameModel GameModel => _gameModel;
        
        public int Try {
            get => _gameModel.Try;
            set => _gameModel.Try = value;
        }
        
        [Inject]
        public void Init(SignalService signalService)
        {
            Debug.Log("GameController Init");
            _signalService = signalService;
            _signalService.Subscribe<StartGameSignal>(StartGame);
        }

        private void StartGame(StartGameSignal signal)
        {
            _gameModel.PlayerName = signal.PlayerName;
            _gameModel.SelectedLevel = signal.SelectedLevel;
            _gameModel.PairsCount = signal.PairsCount;
            _gameModel.TimePerPair = signal.TimePerPair;

            GameModel.ElapsedTime = 0;
        }
        
        public void TickTimer(float deltaTime)
        {
            GameModel.ElapsedTime += deltaTime;

            if (GameModel.ElapsedTime >= GameModel.TimePerPair)
            {
                GameModel.ElapsedTime = GameModel.TimePerPair; 
                _signalService.Send(new GameOverSignal { });
            }
        }
    }
}
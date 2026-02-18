using ShootCommon.SignalSystem;
using Zenject.GameControl.Signals;

namespace Zenject.GameControl
{
    public class GameController
    {
        private SignalService _signalService;
        
        private GameModel _gameModel = new GameModel();
        
        public GameModel GameModel => _gameModel;
        
        [Inject]
        public void Init(SignalService signalService)
        {
            _signalService = signalService;
            _signalService.Subscribe<StartGameSignal>(StartGame);
        }

        private void StartGame(StartGameSignal signal)
        {
            _gameModel.PlayerName = signal.PlayerName;
            _gameModel.SelectedLevel = signal.SelectedLevel;
            _gameModel.PairsCount = signal.PairsCount;
            _gameModel.TimePerPair = signal.TimePerPair;
        }
        
    }
}
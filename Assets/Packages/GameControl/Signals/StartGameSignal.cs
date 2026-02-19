using ShootCommon.SignalSystem;

namespace Packages.GameControl.Signals
{
    public class StartGameSignal : Signal
    {
        public string PlayerName;
        public int SelectedLevel;
        public int PairsCount;
        public float TimePerPair;
    }
}
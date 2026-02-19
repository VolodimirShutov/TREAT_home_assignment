namespace Packages.GameControl
{
    public class GameModel
    {
        public string PlayerName;
        public int SelectedLevel;
        public int PairsCount;
        public float TimePerPair;

        public int Try;
        public float ElapsedTime = 0f;
    }
}
using System.Collections.Generic;

namespace FirebaseModul.Configs
{
    [System.Serializable]
    public class ConfigsModel
    {
        public List<ConfigModel> levels = new List<ConfigModel>() ;
        
        public int min_pairs = 2;
        public int max_pairs = 9;
        public float timer_curve = 1.2f;
        public string default_player_name = "Guest";
    }
}
namespace FirebaseModul.Configs
{
    public interface IGameConfigController
    {
        ConfigsModel Config { get; } 
        void RequestConfigUpdate();   
        ConfigModel GetLevelConfig(int level); 
    }
}
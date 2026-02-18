using FirebaseModul.Configs;
using UnityEngine;
using Zenject;

namespace FirebaseModul
{
    public class FirebaseInstaller : Installer<FirebaseInstaller>
    {
        public override void InstallBindings()
        {
            Debug.Log("FirebaseInstaller install");
            
            Container.Bind<GameDifficultyConfig>()
                .FromResource("GameDifficultyConfig") 
                .AsSingle();
            
            Container.Bind<IGameConfigController>()
                .To<GameConfigController>()
                .AsSingle()
                .NonLazy(); 
            
            Container.Bind<FirebaseController>().AsSingle().NonLazy();
        }
    }
}
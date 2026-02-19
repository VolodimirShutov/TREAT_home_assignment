using FirebaseModul;
using Packages.GameControl;
using ShootCommon;
using UnityEngine;

namespace Zenject
{
    public class CommonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ShootCommonInstaller.Install(Container);
            FirebaseInstaller.Install(Container);
            
            Container.Bind<GameController>()
                .AsSingle()
                .NonLazy();
            
            GlobalStateMachineInstaller.Install(Container);
        }
    }
}
using FirebaseModul;
using ShootCommon;
using UnityEngine;
using Zenject.GameControl;

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
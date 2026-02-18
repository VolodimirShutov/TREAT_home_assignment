using FirebaseModul;
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
            
            GlobalStateMachineInstaller.Install(Container);
        }
    }
}
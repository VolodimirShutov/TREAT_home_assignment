using System.ComponentModel;
using Packages.GlobalStates;
using ShootCommon.GlobalStateMachine;
using Zenject;

namespace ShootCommon
{
    public class GlobalStateMachineInstaller : Installer<GlobalStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindState<InitState>();
            
            Container.BindInterfacesTo<StateMachineController>().AsSingle();
        }

    }
}
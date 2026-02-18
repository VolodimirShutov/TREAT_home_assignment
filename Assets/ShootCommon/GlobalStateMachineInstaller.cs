using System.ComponentModel;
using Packages.GlobalStates;
using ShootCommon.GlobalStateMachine;
using UnityEngine;
using Zenject;

namespace ShootCommon
{
    public class GlobalStateMachineInstaller : Installer<GlobalStateMachineInstaller>
    {
        public override void InstallBindings()
        {
            Debug.Log(" GlobalStateMachine Installer");
            Container.BindState<InitState>();
            Container.BindState<StartState>();
            Container.BindState<GetConfigState>();
            Container.BindState<ShowMainMenuState>();
            
            Debug.Log(" GlobalStateMachine Installer State binded");
            
            Container.BindInterfacesTo<StateMachineController>().AsSingle();
        }

    }
}
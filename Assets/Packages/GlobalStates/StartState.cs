using Cysharp.Threading.Tasks;
using Packages.GlobalStates.StatesSignals;
using ShootCommon.GlobalStateMachine;
using Stateless;
using UnityEngine;

namespace Packages.GlobalStates
{
    public class StartState : GlobalState
    {
        protected override void Configure()
        {
            Permit<GetConfigState>(StateMachineTriggers.GetConfigState);
        }
        
        protected override void OnEntry(StateMachine<IState, StateMachineTriggers>.Transition transition = null)
        {
            Debug.Log("OnEntry StartState");
            SignalService.Send(new ShowPreloaderSignal());
            DelayAndFire().Forget();
        }
        
        private async UniTaskVoid DelayAndFire()
        {
            await UniTask.NextFrame();
            Fire(StateMachineTriggers.GetConfigState);
        }
        
        
        
    }
}
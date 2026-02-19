using Cysharp.Threading.Tasks;
using FirebaseModul;
using Packages.GlobalStates.StatesSignals;
using ShootCommon.GlobalStateMachine;
using Stateless;
using UnityEngine;
using Zenject;

namespace Packages.GlobalStates
{
    public class StartState : GlobalState
    {
        private FirebaseController _firebaseController;
        
        [Inject]
        public void Init(FirebaseController firebaseController)
        {
            _firebaseController = firebaseController;
        }
        
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
            _firebaseController.Initialize();
            await UniTask.NextFrame();
            Fire(StateMachineTriggers.GetConfigState);
        }
        
        
        
    }
}
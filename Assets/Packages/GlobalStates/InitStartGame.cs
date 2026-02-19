using Cysharp.Threading.Tasks;
using FirebaseModul;
using ShootCommon.GlobalStateMachine;
using ShootCommon.InteractiveObjectsSpawnerService;
using Stateless;
using Zenject;

namespace Packages.GlobalStates
{
    public class InitStartGame: GlobalState
    {
        protected override void Configure()
        {
            Permit<GameState>(StateMachineTriggers.GameState);
        }

        
        protected override void OnEntry(StateMachine<IState, StateMachineTriggers>.Transition transition = null)
        {
            InitLayers().Forget();
        }
        
        private async UniTaskVoid InitLayers()
        {
            InteractiveObjectsManager.Instance.Instantiate("GameLevel", "GameContainer");
            await UniTask.NextFrame();
            
            Fire(StateMachineTriggers.GameState);
        }
    }
}
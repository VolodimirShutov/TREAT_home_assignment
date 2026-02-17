using System;
using ShootCommon.SignalSystem;
using UnityEngine;
using Zenject;
using Stateless;
using ShootCommon.GlobalStateMachine.StatesSignals;

namespace ShootCommon.GlobalStateMachine
{
    public class GlobalState : IState, IInitializable, IDisposable
    {
        protected CompositeDisposable DisposeOnExit { get; private set; }
        protected SignalService SignalService { get; private set; }
        private StateMachine<IState, StateMachineTriggers>.StateConfiguration _stateConfiguration;
        private IStateMachineController _stateMachine;

        protected StateMachineTriggers GetCurrentState => _stateMachine.CurrentState;
        
        [Inject]
        public void Init(SignalService signalService,
            IStateMachineController stateMachine)
        {
            SignalService = signalService;
            _stateMachine = stateMachine;
        }

        public virtual void Initialize()
        {
            _stateConfiguration = _stateMachine.Configure(this)
                .OnEntry(OnEntryInternal)
                .OnExit(OnExitInternal);
            Configure();
        }

        public virtual void Dispose()
        {
            OnExitInternal();            
        }

        protected virtual void Configure()
        {
            
        }

        private void OnEntryInternal(StateMachine<IState, StateMachineTriggers>.Transition transition)
        {
            DisposeOnExit = new CompositeDisposable();
            OnEntry(transition);
        }
        
        private void OnExitInternal()
        {
            OnExit();
            DisposeOnExit?.Dispose();
        }
        
        protected virtual void OnEntry(StateMachine<IState, StateMachineTriggers>.Transition transition = null)
        {
        }
        
        protected virtual void OnExit()
        {
        }
        
        protected void Fire(StateMachineTriggers trigger)
        {
            Debug.Log(" Fire " + trigger);
            _stateMachine.FireState(trigger);
            SignalService.Send(new ChangeStateSignal(){SelectedState = GetCurrentState});
        }

        protected void Permit<TState>(StateMachineTriggers trigger)
            where TState: IState
        {
            _stateConfiguration.Permit(trigger, _stateMachine.GetState<TState>());
        }
        
        protected void SubstateOf<TState>() where TState : IState
        {
            _stateConfiguration.SubstateOf(_stateMachine.GetState<TState>());
        }
        
        protected void SubscribeToSignal<T>(Action<T> onSignalTriggered) where T : Signal
        {
            SignalService.Subscribe<T>(onSignalTriggered).AddTo(DisposeOnExit);
        }
        
    }
}
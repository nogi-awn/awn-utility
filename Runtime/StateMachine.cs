using System.Collections.Generic;

namespace AwnUtility
{
    public class StateMachine<TOwner>
    {
        public abstract class State
        {
            protected StateMachine<TOwner> stateMachine => machine;
            protected TOwner owner => machine.owner;

            internal StateMachine<TOwner> machine;
            internal Dictionary<int, State> transitions = new();

            internal void Enter(State prevState)
            {
                OnEnter(prevState);
            }
            internal void Update()
            {
                OnUpdate();
            }
            internal void Exit(State nextState)
            {
                OnExit(nextState);
            }

            protected virtual void OnEnter(State prevState) {}
            protected virtual void OnUpdate() {}
            protected virtual void OnExit(State nextState) {}
        }

        public sealed class AnyState : State { }
        public TOwner owner { get; }
        public State currentState { get; private set; }

        private LinkedList<State> stateList = new LinkedList<State>();

        public StateMachine(TOwner owner)
        {
            this.owner = owner;
        }

        public void AddTransition<TFrom, TTo>(int triggerId)
            where TFrom : State, new()
            where TTo : State, new()
        {
            var from = GetOrAddState<TFrom>();
            if(from.transitions.ContainsKey(triggerId))
            {
                throw new System.ArgumentException();
            }
            var to = GetOrAddState<TTo>();
            from.transitions.Add(triggerId, to);
        }

        public void Trigger(int triggerId)
        {
            State to;
            if(!currentState.transitions.TryGetValue(triggerId, out to))
            {
                if(!GetOrAddState<AnyState>().transitions.TryGetValue(triggerId, out to))
                {
                    return;
                }
            }
            ChangeState(to);
        }
        public void Start<TFirst>()
            where TFirst : State, new()
        {
            Start(GetOrAddState<TFirst>());
        }
        public void Start(State firstState)
        {
            currentState = firstState;
            currentState.Enter(null);
        }

        public void Update() {
            currentState.Update();
        }

        private T AddState<T>() where T : State, new()
        {
            var state = new T();
            state.machine = this;
            stateList.AddLast(state);

            return state;
        }
        private T GetOrAddState<T>() where T : State, new()
        {
            foreach(var state in stateList)
            {
                if(state is T result)
                    return result;
            }
            return AddState<T>();
        }
        private void ChangeState(State nextState)
        {
            currentState.Exit(nextState);
            currentState = nextState;
            currentState.Enter(nextState);
        }
    }
}
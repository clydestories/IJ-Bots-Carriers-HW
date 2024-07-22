using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiniteStateMachine
{
    private IState _currentState;

    private Dictionary<Type, IState> _states;

    public FiniteStateMachine(Dictionary<Type, IState> states)
    {
        _states = states;
    }

    public void EnterState<TState>()
    {
        Debug.Log(typeof(TState));
        EnterState(typeof(TState));
    }

    public void EnterState(Type type)
    {
        if (_states.TryGetValue(type, out var state))
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }

    public void UpdateState()
    {
        _currentState.Update();

        if (_currentState.TryGetNextState(out Type type))
        {
            EnterState(type);
        }

        Debug.Log($"{_currentState.GetType()}");
    }
}
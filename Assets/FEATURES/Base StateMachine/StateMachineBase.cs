using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class StateMachineBase<TState, TMachine> : MonoBehaviour
    where TState : IState
{
    public TState CurrentState { get; protected set; }
    public Dictionary<Type, TState> states;

    protected virtual void Update()
    {
        CurrentState?.UpdateState();
    }

    public void ChangeState(System.Type type)
    {
        try
        {
            TState newState = states[type];

            CurrentState?.OnExit();
            CurrentState = newState;
            CurrentState.OnEnter();
        }
        catch
        {
            Debug.LogError("Error changing state, likely invalid state type.");
        }
    }
}

public interface IState
{
    public virtual void OnEnter() { }
    public virtual void OnExit() { }
    public virtual void UpdateState() { }
}

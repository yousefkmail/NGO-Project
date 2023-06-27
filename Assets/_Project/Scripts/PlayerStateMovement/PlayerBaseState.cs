using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    protected PlayerControllerState _context;
    protected PlayerStateFactory _states;

    protected PlayerBaseState _currentSubState;
    protected PlayerBaseState _currentSuperState;
    protected bool _isBaseState;

    public event Action<PlayerState> OnStateChange;

    public PlayerBaseState(PlayerControllerState context, PlayerStateFactory factory)
    {

        _context = context;

        _states = factory;

    }



    public abstract void CheckSwitchState();
    public abstract void UpdateState();

    public abstract void EnterState();
    public abstract void ExitState();

    public abstract void InitializeSubState();


    public void UpdateStates()
    {
        if (_currentSubState != null)
            _currentSubState.UpdateState();
        UpdateState();

    }


    protected void SetSubState(PlayerBaseState newSubState)
    {
        _currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        _currentSuperState = newSuperState;

    }
    protected void SwtichState(PlayerState newState)
    {

        ExitState();
        OnStateChange?.Invoke(newState);
        _states._states[newState].EnterState();
        if (_isBaseState)
            _context.CurrentState = _states._states[newState];
        else if (_currentSuperState != null)
        {
            _currentSuperState.SetSubState(_states._states[newState]);

        }
    }

}

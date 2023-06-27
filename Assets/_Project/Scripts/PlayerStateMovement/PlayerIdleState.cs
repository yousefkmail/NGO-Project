using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{

    public PlayerIdleState(PlayerControllerState context, PlayerStateFactory factory) : base(context, factory)
    {

        _isBaseState = false;

    }

    public override void CheckSwitchState()
    {

        if (_context._input.MoveValue.magnitude > 0)
        {

            SwtichState(PlayerState.Walk);
        }

    }

    public override void EnterState()
    {
        Debug.Log("I am in idle state");
    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {

        CheckSwitchState();

    }
}

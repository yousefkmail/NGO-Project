using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerControllerState context, PlayerStateFactory factory) : base(context, factory)
    {

        _isBaseState = true;

    }

    public override void CheckSwitchState()
    {
        if (_context.IsGrounded)
        {

            SwtichState(PlayerState.Grounded);

        }
    }

    public override void EnterState()
    {
        Debug.Log("We entered falling");
        InitializeSubState();
        _context.gravity = -1f;

    }

    public override void ExitState()
    {
    }

    public override void InitializeSubState()
    {

        if (_context._input.MoveValue.magnitude > 0.1f)
        {
            SetSubState(_states.Walk());
        }
        else
            SetSubState(_states.Idle());


    }

    public override void UpdateState()
    {
        handleFalling();
        CheckSwitchState();
    }

    private void handleFalling()
    {
        _context.gravity += -9.81f * Time.deltaTime * _context.FallAcceleration;
        _context.Move(new Vector3(0, _context.gravity, 0) * Time.deltaTime);
    }
}

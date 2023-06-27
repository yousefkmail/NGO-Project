using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerControllerState context, PlayerStateFactory factory) : base(context, factory)
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
        Debug.Log("I jumped");
        InitializeSubState();
        _context.gravity = 10 * _context.JumpHeight;
        _context.Move(new Vector3(0, 0.1f, 0));
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
        _context.gravity += -9.81f * _context.FallAcceleration * Time.deltaTime;
        _context.Move(new Vector3(0, _context.gravity, 0) * Time.deltaTime);
        CheckSwitchState();
    }


}

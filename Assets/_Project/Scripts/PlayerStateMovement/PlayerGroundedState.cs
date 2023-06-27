using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{

    public PlayerGroundedState(PlayerControllerState context, PlayerStateFactory factory) : base(context, factory)
    {
        _isBaseState = true;

    }
    public override void CheckSwitchState()
    {
        if (_context._input.Jump)
        {
            SwtichState(PlayerState.Jump);
        }
        if (!_context.IsGrounded)
        {

            SwtichState(PlayerState.Fall);

        }

    }

    public override void EnterState()
    {
        Debug.Log("I am in grounded state");
        InitializeSubState();
        _context.gravity = -10f;

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
        _context.Move(new Vector3(0, _context.gravity, 0) * Time.deltaTime);
        CheckSwitchState();

    }
}

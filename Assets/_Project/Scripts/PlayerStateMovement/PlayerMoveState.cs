using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{

    public PlayerMoveState(PlayerControllerState context, PlayerStateFactory factory) : base(context, factory)
    {

        _isBaseState = false;

    }

    public override void CheckSwitchState()
    {

        if (_context._input.MoveValue.magnitude < 0.1f)
        {
            SwtichState(PlayerState.Idle);
        }
    }

    public override void EnterState()
    {
        Debug.Log("I am in move state");
    }

    public override void ExitState()
    {

    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {

        var _targetRotation = Mathf.Atan2(_context._input.MoveValue.x, _context._input.MoveValue.y) * Mathf.Rad2Deg +
              _context._input.CameraRotationY;

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        _context.Move(targetDirection * 10 * _context.MoveSpeed * Time.deltaTime);
        CheckSwitchState();
    }





}

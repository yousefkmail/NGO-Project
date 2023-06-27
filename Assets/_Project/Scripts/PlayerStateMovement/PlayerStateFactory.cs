using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{

    Idle,
    Walk,
    Jump,
    Fall,
    Grounded


}

public class PlayerStateFactory
{

    PlayerControllerState _context;
    public Dictionary<PlayerState, PlayerBaseState> _states = new Dictionary<PlayerState, PlayerBaseState>();

    public PlayerStateFactory(PlayerControllerState context)
    {

        _context = context;
        _states[PlayerState.Idle] = new PlayerIdleState(_context, this);
        _states[PlayerState.Walk] = new PlayerMoveState(_context, this);
        _states[PlayerState.Jump] = new PlayerJumpState(_context, this);
        _states[PlayerState.Fall] = new PlayerFallState(_context, this);
        _states[PlayerState.Grounded] = new PlayerGroundedState(_context, this);

    }

    public PlayerBaseState Idle()
    {

        return _states[PlayerState.Idle];

    }
    public PlayerBaseState Walk()
    {

        return _states[PlayerState.Walk];

    }

    public PlayerBaseState Grounded()
    {

        return _states[PlayerState.Grounded];

    }


    public PlayerBaseState Fall()
    {

        return _states[PlayerState.Fall];

    }
    public PlayerBaseState Jump()
    {

        return _states[PlayerState.Jump];

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerControllerState : MonoBehaviour
{

    [SerializeField]
    private float _moveSpeed = 1;

    [SerializeField]
    private float _jumpHeight = 1f;

    [SerializeField]
    private float _fallAcceleration = 1;

    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float JumpHeight { set { _jumpHeight = value; } get { return _jumpHeight; } }
    public float FallAcceleration { get { return _fallAcceleration; } set { _fallAcceleration = value; } }
    [HideInInspector]
    public ServerInputReceiver _input;

    public PlayerBaseState CurrentState { set; get; }


    PlayerStateFactory _states;
    // Start is called before the first frame update

    public event Action OnplayerJump;
    public event Action OnPlayerMove;
    public event Action OnPlayerGrounded;
    public event Action OnPlayerIdle;
    public event Action OnPlayerFall;

    public float gravity = -0.2f;

    public bool IsGrounded = true;

    float TurnSmoothVelociry = 0;

    void Awake()
    {
        _input = GetComponent<ServerInputReceiver>();
        _states = new PlayerStateFactory(this);
        CurrentState = _states.Grounded();
        CurrentState.EnterState();
    }
    void Update()
    {
        CurrentState.UpdateStates();
        IsGrounded = GetComponent<CharacterController>().isGrounded;
        HandleRotation();
    }

    void OnEnable()
    {
        _states.Idle().OnStateChange += StateEntered;
        _states.Grounded().OnStateChange += StateEntered;
        _states.Jump().OnStateChange += StateEntered;
        _states.Walk().OnStateChange += StateEntered;
        _states.Fall().OnStateChange += StateEntered;

    }
    void OnDisable()
    {


        _states.Idle().OnStateChange -= StateEntered;
        _states.Grounded().OnStateChange -= StateEntered;
        _states.Jump().OnStateChange -= StateEntered;
        _states.Walk().OnStateChange -= StateEntered;
        _states.Fall().OnStateChange -= StateEntered;
    }

    private void GroundedChanged(bool previousValue, bool newValue)
    {
        IsGrounded = newValue;
    }

    private void HandleRotation()
    {
        if (new Vector2(_input.MoveValue.x, _input.MoveValue.y).magnitude < 0.1f) return;
        var _targetRotation = Mathf.Atan2(_input.MoveValue.x, _input.MoveValue.y) * Mathf.Rad2Deg +
             _input.CameraRotationY;
        var _smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref TurnSmoothVelociry, 0.1f);
        Quaternion targetDirection = Quaternion.Euler(0.0f, _smoothAngle, 0.0f);
        transform.rotation = targetDirection;

    }



    public void StateEntered(PlayerState state)
    {

        switch (state)
        {

            case PlayerState.Fall:
                OnPlayerFall?.Invoke();
                break;

            case PlayerState.Grounded:
                OnPlayerGrounded?.Invoke();
                break;
            case PlayerState.Idle:
                OnPlayerIdle?.Invoke();
                break;
            case PlayerState.Jump:
                OnplayerJump?.Invoke();
                break;
            case PlayerState.Walk:
                OnPlayerMove?.Invoke();
                break;
            default:
                break;



        }

    }



    public void Move(Vector3 position)
    {
        GetComponent<CharacterController>().Move(position);
    }



}

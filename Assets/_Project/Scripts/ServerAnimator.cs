using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class ServerAnimator : NetworkBehaviour
{
    [SerializeField] float TransitionSpeedBetweenWalkAndrun = 5;
    [SerializeField] Animator animator;
    [SerializeField] NetworkAnimator networkAnimator;

    [SerializeField] PlayerControllerState playerControllerState;

    float MoveSpeedAnimationValue = 0;
    float LastSpeedAnimationValue = 0;

    void Update()
    {
        animator.SetFloat("Blend", MoveSpeedAnimationValue);

        if (MoveSpeedAnimationValue != LastSpeedAnimationValue)
        {
            MoveSpeedAnimationValue += (LastSpeedAnimationValue - MoveSpeedAnimationValue) * Time.deltaTime * TransitionSpeedBetweenWalkAndrun;
        }

    }
    void OnEnable()
    {

        playerControllerState.OnPlayerMove += PlayerMoved;
        playerControllerState.OnPlayerFall += PlayerFell;
        playerControllerState.OnplayerJump += PlayerJumped;
        playerControllerState.OnPlayerGrounded += PlayerGrounded;
        playerControllerState.OnPlayerIdle += PlayerIdle;
    }

    void OnDisable()
    {

        playerControllerState.OnPlayerMove += PlayerMoved;
        playerControllerState.OnPlayerFall += PlayerFell;
        playerControllerState.OnplayerJump += PlayerJumped;
        playerControllerState.OnPlayerGrounded += PlayerGrounded;
        playerControllerState.OnPlayerIdle += PlayerIdle;

    }

    private void PlayerIdle()
    {
        SetAnimation(0);

    }

    private void PlayerJumped()
    {
        networkAnimator.SetTrigger("Jump");
        networkAnimator.ResetTrigger("Grounded");

    }

    private void PlayerGrounded()
    {
        networkAnimator.SetTrigger("Grounded");
        networkAnimator.ResetTrigger("Jump");

    }

    private void PlayerFell()
    {
        networkAnimator.SetTrigger("Fall");
    }

    private void PlayerMoved()
    {
        SetAnimation(1);

    }
    void SetAnimation(float value)
    {

        LastSpeedAnimationValue = value;

    }
}

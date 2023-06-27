using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerEnabledAnimator : MonoBehaviour
{
    [Tooltip("After how many seconds does the character wave after spawning")]
    [SerializeField] private float waveDelay = 3;

    [SerializeField] private float FullWaveTime = 5;

    [SerializeField] int WaveLayerIndex = 1;

    [Range(0, 1)]
    [Tooltip("range from 0 - 1 , 0 is left and 1 is right")]
    [SerializeField] float WaveDirection = 0.5f;

    Animator animator;

    private float CurrentWaveLayerWeight = 0;

    private bool IsWaving = false;

    private Coroutine coroutine;
    void Awake()
    {

        animator = GetComponent<Animator>();

    }
    void OnEnable()
    {
        coroutine = StartCoroutine(StartWaveOperation());
    }

    void OnDisable()
    {
        StopCoroutine(coroutine);
        IsWaving = false;
        CurrentWaveLayerWeight = 0;

    }

    IEnumerator StartWaveOperation()
    {

        yield return new WaitForSeconds(waveDelay);
        IsWaving = true;

        StartCoroutine(StopWaveOperation());


    }

    IEnumerator StopWaveOperation()
    {
        yield return new WaitForSeconds(FullWaveTime);
        IsWaving = false;
    }

    void Update()
    {
        animator.SetFloat("WaveBlend", WaveDirection);
        animator.SetLayerWeight(WaveLayerIndex, CurrentWaveLayerWeight);


        if (IsWaving)
        {
            if (CurrentWaveLayerWeight < 1)
                CurrentWaveLayerWeight += Time.deltaTime;

        }
        else
        {
            if (CurrentWaveLayerWeight > 0)
                CurrentWaveLayerWeight -= Time.deltaTime;


        }




    }




}

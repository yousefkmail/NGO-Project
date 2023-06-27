using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class GameLobbySeatChangerequest : ScriptableObject
{
    public UnityAction<int> OnEventRaise;
    public void Raise(int value)
    {
        OnEventRaise?.Invoke(value);
    }
}

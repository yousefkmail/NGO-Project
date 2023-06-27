using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MessageViewUI : MonoBehaviour
{
    protected Message message;

    public string ConvertFloatToTime(float x)
    {
        string value = "";
        int min = (int)x / 60;
        value += min;
        value += ":";
        value += ((int)(x % 60));
        return value;
    }
    private static bool showTimeStamp;

    public static bool ShowTimeStamp
    {
        get { return showTimeStamp; }
        set
        {
            if (showTimeStamp != value)
            {
                showTimeStamp = value;
                OnTimeStampChanged?.Invoke();
            }
        }
    }

    public static event Action OnTimeStampChanged;

    public void ShowMessage(Message message)
    {

        this.message = message;
        UpdateVisuals();
    }
    public abstract void UpdateVisuals();

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SystemMessageViewUI : MessageViewUI
{

    [SerializeField] TextMeshProUGUI messageValue;

    public static Color color;

    public override void UpdateVisuals()
    {
        string timestamp = ConvertFloatToTime(message.time);
        messageValue.color = color;
        this.messageValue.text = ShowTimeStamp ? $"{timestamp}  {message.value}" : message.value;

    }



}

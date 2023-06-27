using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatMessageViewUI : MessageViewUI
{
    public static bool ShowChannelName = true;
    [SerializeField] TextMeshProUGUI messageValue;

    public static Color color;



    public override void UpdateVisuals()
    {
        string messageValue = "";
        if (ShowTimeStamp)
        {
            messageValue += ConvertFloatToTime(message.time);
        }
        messageValue += $" {message.sender}({message.channel}): {message.value}";

        this.messageValue.color = color;
        this.messageValue.text = messageValue;

    }
}

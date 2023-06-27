using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public struct Message
{
    public string value;
    public string sender;
    public float time;
    public string channel;

}
public class GameConsoleLogger : MonoBehaviour
{
    [SerializeField] MessageViewUI SystemMessage;

    [SerializeField] MessageViewUI ChatMessage;

    [SerializeField] Transform MessagesContainer;

    [SerializeField] Scrollbar scrollBar;

    [SerializeField] bool ShowTimeStamp = true;
    [SerializeField] Color SystemColor;
    [SerializeField] Color ChatColor;
    [Tooltip("Number of messages that can appear in the list. (less than 0 means unlimited)")]
    [SerializeField] int MessagesLimit = 50;

    Queue<MessageViewUI> messages = new Queue<MessageViewUI>();

    void OnEnable()
    {
        MessageViewUI.ShowTimeStamp = ShowTimeStamp;
        ChatMessageViewUI.color = ChatColor;
        SystemMessageViewUI.color = SystemColor;
        ConsoleMessageSO.OnMessageAddRequest += MessageReceived;
        MessageViewUI.OnTimeStampChanged += ShowTimeStampChanged;
    }
    void OnDisable()
    {
        ConsoleMessageSO.OnMessageAddRequest -= MessageReceived;
        MessageViewUI.OnTimeStampChanged -= ShowTimeStampChanged;

    }

    private void ShowTimeStampChanged()
    {
        foreach (MessageViewUI message in messages)
        {
            message.UpdateVisuals();
        }
    }

    private void MessageReceived(Message arg0)
    {
        HandleMessageReceived(arg0);
    }

    private void HandleMessageReceived(Message message)
    {
        switch (message.channel)
        {
            case "System":
                AddSystemMessage(message);
                break;
            case "Chat":
                addChatMessage(message);
                break;

            default: break;

        }
    }
    void addChatMessage(Message message)
    {
        AddMessage(ChatMessage, message);
    }
    void AddSystemMessage(Message message)
    {
        AddMessage(SystemMessage, message);
    }

    void AddMessage(MessageViewUI MessageView, Message message)
    {
        MessageViewUI messageUI = Instantiate(MessageView, MessagesContainer);
        message.time = Time.time;
        messageUI.ShowMessage(message);
        messages.Enqueue(messageUI);
        if (messages.Count > MessagesLimit)
            Destroy(messages.Dequeue().gameObject);
        StartCoroutine(SetBottom());

    }
    IEnumerator SetBottom()
    {
        Debug.Log("asdasdasdasd");
        yield return new WaitForEndOfFrame();
        scrollBar.value = 0;
    }

}

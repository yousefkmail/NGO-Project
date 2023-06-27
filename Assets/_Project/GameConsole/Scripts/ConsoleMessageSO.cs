using UnityEngine;
using UnityEngine.Events;

public class ConsoleMessageSO
{

    public static UnityAction<Message> OnMessageAddRequest;


    public static void AddMessageRaise(Message message)
    {

        OnMessageAddRequest?.Invoke(message);

    }

}
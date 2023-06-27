using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Vivox;
using UnityEngine;
using VivoxUnity;
using TMPro;
using UnityEngine.UI;
using System.ComponentModel;

public class VivoxHandler : MonoBehaviour
{
    public static VivoxHandler instance;
    IChannelSession _channelSession;
    ILoginSession LoginSession;
    Account account;

    [SerializeField] bool connectAudio = false;
    [SerializeField] bool connectText = true;

    string currentChannel = "Chat";
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(transform);
            instance = this;

        }
        else
        {
            Destroy(transform);
        }
        VivoxService.Instance.Initialize();
        Login(PlayerPrefs.GetString("PlayerName"));
    }

    void OnEnable()
    {
        GameConsoleInputField.OnMessageSent += SendGroupMessage;

    }

    void OnDisable()
    {
        GameConsoleInputField.OnMessageSent -= SendGroupMessage;

    }

    public void JoinChannel(string channelName, ChannelType channelType, bool connectAudio, bool connectText, bool transmissionSwitch = true, Channel3DProperties properties = null)
    {
        if (LoginSession.State == LoginState.LoggedIn)
        {
            Channel channel = new Channel(channelName, channelType, properties);
            ConsoleMessageSO.AddMessageRaise(new Message()
            {
                channel = "System",
                value = "Connecting to chat...",

            });

            _channelSession = LoginSession.GetChannelSession(channel);
            _channelSession.MessageLog.AfterItemAdded += onChannelMessageReceived;

            _channelSession.PropertyChanged += OnChannelPropertyChanged;
            _channelSession.Participants.AfterKeyAdded += OnParticipantAdded;
            _channelSession.Participants.BeforeKeyRemoved += OnParticipantRemoved;
            _channelSession.Participants.AfterValueUpdated += OnParticipantValueUpdated;

            _channelSession.BeginConnect(connectAudio, connectText, transmissionSwitch, _channelSession.GetConnectToken(), ar =>
            {
                try
                {
                    _channelSession.EndConnect(ar);
                    ConsoleMessageSO.AddMessageRaise(new Message()
                    {
                        channel = "System",
                        value = "Connection successful",

                    });
                }
                catch (Exception e)
                {
                    Debug.LogError($"Could not connect to channel: {e.Message}");
                    ConsoleMessageSO.AddMessageRaise(new Message()
                    {
                        channel = "System",
                        value = "Could not connect to the chat",

                    });


                    return;
                }
            });
        }
        else
        {
            Debug.LogError("Can't join a channel when not logged in.");
        }
    }

    private void OnParticipantValueUpdated(object sender, ValueEventArg<string, IParticipant> e)
    {

    }

    private void OnParticipantRemoved(object sender, KeyEventArg<string> e)
    {
    }

    private void OnParticipantAdded(object sender, KeyEventArg<string> e)
    {

    }

    private void OnChannelPropertyChanged(object sender, PropertyChangedEventArgs e)
    {

    }

    public void Login(string displayName = null)
    {
        account = new Account(displayName);
        ConsoleMessageSO.AddMessageRaise(new Message()
        {
            channel = "System",
            value = "Logging into vivox...",

        });
        LoginSession = VivoxService.Instance.Client.GetLoginSession(account);
        LoginSession.PropertyChanged += LoginSession_PropertyChanged;

        LoginSession.BeginLogin(LoginSession.GetLoginToken(), SubscriptionMode.Accept, null, null, null, ar =>
        {
            try
            {

                LoginSession.EndLogin(ar);
                ConsoleMessageSO.AddMessageRaise(new Message()
                {
                    channel = "System",
                    value = "Logging successfully",

                });

            }
            catch (Exception e)
            {
                Debug.Log(e);
                // Unbind any login session-related events you might be subscribed to.
                // Handle error
                ConsoleMessageSO.AddMessageRaise(new Message()
                {
                    channel = "System",
                    value = "Logging Error",

                });
                return;
            }

        });
    }


    private void LoginSession_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        var loginSession = (ILoginSession)sender;
        if (e.PropertyName == "State")
        {
            if (loginSession.State == LoginState.LoggedIn)
            {


                JoinChannel("TestChannel", ChannelType.NonPositional, connectAudio, connectText);
            }
        }
    }




    void SendGroupMessage(string message)
    {
        if (_channelSession == null)
        {
            ConsoleMessageSO.AddMessageRaise(new Message()
            {
                channel = "System",
                value = "Please wait until connecting to chat is successful",

            });
            return;

        }

        if (_channelSession.ChannelState != ConnectionState.Connected)
        {
            ConsoleMessageSO.AddMessageRaise(new Message()
            {
                channel = "System",
                value = "Please wait until connecting to chat is successful",

            });
            return;
        }


        _channelSession.BeginSendText(message, ar =>
        {
            try
            {
                _channelSession.EndSendText(ar);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                // Handle error
                return;
            }
        });
    }
    // Receiving
    void onChannelMessageReceived(object sender, QueueItemAddedEventArgs<IChannelTextMessage> queueItemAddedEventArgs)
    {
        var senderName = queueItemAddedEventArgs.Value.Sender.DisplayName;
        var message = queueItemAddedEventArgs.Value.Message;
        ConsoleMessageSO.AddMessageRaise(new Message()
        {
            channel = currentChannel,
            sender = senderName,
            value = message,

        });
    }






}

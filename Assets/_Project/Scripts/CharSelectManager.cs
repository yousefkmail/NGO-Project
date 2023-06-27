using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.SceneManagement;

public class CharSelectManager : NetworkBehaviour
{

    [SerializeField] SeatsManager seatsManager;

    [SerializeField] GameLobbySeatChangerequest request;

    [SerializeField] LobbyPlayersGraphics lobbyPlayersGraphics;
    public NetworkList<LobbyPlayer> lobbyPlayers;
    void Awake()
    {
        lobbyPlayers = new NetworkList<LobbyPlayer>();

    }



    public override void OnNetworkSpawn()
    {
        request.OnEventRaise += ChangeSeatRequested;
        lobbyPlayers.OnListChanged += ListChanged;
        lobbyPlayersGraphics.UpdatePlayer(lobbyPlayers);
        seatsManager.UpdateSeatsVisuals(lobbyPlayers);
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback += ClientDisconnected;

        }
    }


    private void ClientDisconnected(ulong obj)
    {
        foreach (LobbyPlayer player in lobbyPlayers)
        {

            if (player.PlayerID == obj)
            {
                lobbyPlayers.Remove(player);
            }

        }

    }

    private void ListChanged(NetworkListEvent<LobbyPlayer> changeEvent)
    {
        lobbyPlayersGraphics.UpdatePlayer(lobbyPlayers);
        seatsManager.UpdateSeatsVisuals(lobbyPlayers);
        ConsoleMessageSO.AddMessageRaise(new Message() { value = "Someone changed his seat", channel = "Lobby" });
    }

    public override void OnNetworkDespawn()
    {
        request.OnEventRaise -= ChangeSeatRequested;
        lobbyPlayers.OnListChanged -= ListChanged;
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientDisconnectCallback -= ClientDisconnected;
        }

    }



    private void ChangeSeatRequested(int seatIndex)
    {
        ChangeSeatRequestServerRPC(seatIndex, NetworkManager.Singleton.LocalClientId);

    }
    [ServerRpc(RequireOwnership = false)]
    public void ChangeSeatRequestServerRPC(int seatIndex, ulong clientId)
    {

        // Debug.Log($"client {clientId} switched seat to {seatIndex} ");
        for (int i = 0; i < lobbyPlayers.Count; i++)
        {

            if (lobbyPlayers[i].PlayerID == clientId)
            {

                lobbyPlayers[i] = new LobbyPlayer()
                {
                    PlayerID = clientId,
                    SeatIndex = seatIndex,

                };
                return;
            }



        }

        lobbyPlayers.Add(new LobbyPlayer() { PlayerID = clientId, SeatIndex = seatIndex, });

    }

    public void StartGame()
    {
        if (!IsServer)
        {
            PopUpManager.CreateMessagePopUpUI("Only the host can start the game", "Ok");

            return;
        }
        if (NetworkManager.Singleton.ConnectedClients.Count > lobbyPlayers.Count)
        {
            PopUpManager.CreateMessagePopUpUI("Not everyone selected their character", "Ok");
            return;
        }
        foreach (LobbyPlayer player in lobbyPlayers)
        {

            NetworkObject playerobj = NetworkManager.Singleton.SpawnManager.GetPlayerNetworkObject(player.PlayerID);
            playerobj.GetComponent<PresistantPlayer>().AvatarConfiguration.Value = player.SeatIndex;
        }

        SceneManagerHandler.Instance.LoadGamePlayScene();
    }





}

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class SeatsManager : MonoBehaviour
{

    public LobbySeat[] lobbySeats;

    void Awake()
    {
        for (int i = 0; i < lobbySeats.Length; i++)
        {
            lobbySeats[i].seatIndex = i;
        }
    }
    public void UpdateSeatsVisuals(NetworkList<LobbyPlayer> players)
    {
        foreach (LobbySeat seat in lobbySeats)
        {
            seat.GetComponent<Button>().interactable = true;
        }

        for (int i = 0; i < players.Count; i++)
        {


            lobbySeats[players[i].SeatIndex].GetComponent<Button>().interactable = false;

        }


    }

}

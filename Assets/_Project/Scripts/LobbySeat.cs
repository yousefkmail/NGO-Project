using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class LobbySeat : MonoBehaviour
{
    [HideInInspector]
    public int seatIndex;


    Button button;

    [SerializeField] UnityEvent<int> OnSeatClicked;


    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SeatClicked);

    }

    private void SeatClicked()
    {
        OnSeatClicked?.Invoke(seatIndex);
    }

    public void UpdateVisuals(NetworkList<LobbyPlayer> players)
    {
        foreach (LobbyPlayer player in players)
        {
            Debug.Log(player.SeatIndex);
            if (seatIndex == player.SeatIndex)
            {
                this.GetComponent<Button>().interactable = false;
                return;

            }

        }
        this.GetComponent<Button>().interactable = true;

    }
}

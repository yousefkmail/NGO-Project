using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using TMPro;
using System;
using Unity.Networking.Transport.Relay;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class RelayManager : MonoBehaviour
{
    [SerializeField]
    string JoinCodeErrorDisplay =
    "Error in the join code, Please Make sure you write it correctly";
    [SerializeField] TMP_InputField Joincode;

    [SerializeField] TMP_InputField PlayerName;
    [SerializeField] TextMeshProUGUI JoinCodeError;


    public async void StartHost()
    {
        LoadingPopUpUI popUpUI = null;
        try
        {


            if (string.IsNullOrEmpty(PlayerName.text))
            {
                HandleEmptyPlayerName();
                return;
            }
            popUpUI = PopUpManager.CreateLoadingPopUpUI("Creating a host...");

            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(4);

            var JoinCodeDisplay = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            popUpUI.Close();
            LoadingPopUpUI popUpUI1 = PopUpManager.CreateLoadingPopUpUI("Starting host...");

            PlayerPrefs.SetString("RelayCode", JoinCodeDisplay);
            PlayerPrefs.SetString("PlayerName", PlayerName.text);
            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            popUpUI1.Close();
            NetworkManager.Singleton.StartHost();
        }

        catch (RelayServiceException e)
        {
            if (popUpUI != null) popUpUI.Close();
            PopUpManager.CreateMessagePopUpUI("Coult not create host ", "Confirm");
            Debug.Log(e);

        }



    }



    public async void JoinHostWithCode()
    {
        try
        {
            if (string.IsNullOrEmpty(Joincode.text))
            {
                HandleEmptyJoinCode();
                return;
            }
            if (string.IsNullOrEmpty(PlayerName.text))
            {
                HandleEmptyPlayerName();
                return;
            }

            JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(Joincode.text);
            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");
            PlayerPrefs.SetString("PlayerName", PlayerName.text);
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartClient();

        }

        catch (RelayServiceException e)
        {
            Debug.Log(e);

        }



    }
    private void HandleEmptyPlayerName()
    {
        JoinCodeError.text = "Please enter your name";

    }

    private void HandleEmptyJoinCode()
    {
        JoinCodeError.text = JoinCodeErrorDisplay;
    }
}

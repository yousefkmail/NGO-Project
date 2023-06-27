using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System;

public class Authentication : MonoBehaviour
{
    // Start is called before the first frame update
    InitializationOptions initializationOptions;

    void Start()
    {
        Authenticate();
    }

    public async void Authenticate()
    {
        try
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (AuthenticationException e)
        {
            Debug.Log(e);

        }
        catch (RequestFailedException e)
        {
            Debug.Log(e);
            MessagePopUpUI popUpUI = PopUpManager.CreateMessagePopUpUI("Could not connect to the internet, please connected and restart your game", "Restart");
            if (popUpUI != null)
            {
                popUpUI.OnbuttonClicked += ButtonClicked;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }



    }

    private void ButtonClicked()
    {
        Application.Quit();
    }
}

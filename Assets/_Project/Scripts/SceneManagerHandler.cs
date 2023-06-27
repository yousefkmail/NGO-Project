using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerHandler : NetworkBehaviour
{

    LoadingPopUpUI popUpUI;
    public static SceneManagerHandler Instance;
    [SerializeField] string GamePlayerSceneName = "GamePlay";
    [SerializeField] string CharSelectSceneName = "CharSelect";


    [SerializeField] string CharSelectScenePopUpString = " Loading Character select...";
    [SerializeField] string GamePlayScenePopUpString = "Entering game...";



    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator Start()
    {
        LoadingPopUpUI popup = PopUpManager.CreateLoadingPopUpUI("Loadgin...");
        yield return new WaitUntil(() => UnityServices.State == ServicesInitializationState.Initialized);
        yield return new WaitUntil(() => NetworkManager.Singleton != null && AuthenticationService.Instance != null);
        yield return new WaitUntil(() => AuthenticationService.Instance.IsSignedIn);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        popup.Close();
        NetworkManager.Singleton.OnServerStarted += ServerStarted;
    }

    private void ServerStarted()
    {
        LoadCharSelectScene();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        NetworkManager.Singleton.SceneManager.OnLoad += LoadStarted;
        NetworkManager.Singleton.SceneManager.OnLoadComplete += LoadComplete;

    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        NetworkManager.Singleton.SceneManager.OnLoad -= LoadStarted;
        NetworkManager.Singleton.SceneManager.OnLoadComplete -= LoadComplete;

    }
    private void LoadStarted(ulong clientId, string sceneName, LoadSceneMode loadSceneMode, AsyncOperation asyncOperation)
    {
        switch (sceneName)
        {
            case "CharSelect":
                popUpUI = PopUpManager.CreateLoadingPopUpUI(CharSelectScenePopUpString);
                break;
            case "GamePlay":
                popUpUI = PopUpManager.CreateLoadingPopUpUI(GamePlayScenePopUpString);
                break;
            default:
                popUpUI = PopUpManager.CreateLoadingPopUpUI("Loading");
                break;
        }

    }


    private void LoadComplete(ulong clientId, string sceneName, LoadSceneMode loadSceneMode)
    {
        Debug.Log(clientId);
        if (popUpUI != null)
            popUpUI.Close();
    }

    public void LoadCharSelectScene()
    {
        // popUpUI = PopUpManager.CreateLoadingPopUpUI(CharSelectScenePopUpString);
        LoadNetworkScene(CharSelectSceneName);

    }

    public void LoadGamePlayScene()
    {
        // popUpUI = PopUpManager.CreateLoadingPopUpUI(GamePlayScenePopUpString);
        LoadNetworkScene(GamePlayerSceneName);

    }
    public void LoadNetworkScene(string sceneName)
    {
        NetworkManager.Singleton.SceneManager.LoadScene(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);

    }
}

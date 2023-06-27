using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.SceneManagement;

public class PlayerObjectsSpawner : NetworkBehaviour
{
    [SerializeField]
    private GameObject BasePlayer;

    void Awake()
    {

        NetworkManager.Singleton.SceneManager.OnLoadEventCompleted += SpawnPlayers;

    }

    private void SpawnPlayers(string sceneName, LoadSceneMode loadSceneMode, List<ulong> clientsCompleted, List<ulong> clientsTimedOut)
    {

        if (!IsServer) return;
        foreach (NetworkClient client in NetworkManager.Singleton.ConnectedClientsList)
        {

            int x = client.PlayerObject.GetComponent<PresistantPlayer>().AvatarConfiguration.Value;
            GameObject playerobj = Instantiate(BasePlayer, transform.position, Quaternion.identity);
            playerobj.GetComponent<GraphicsHandler>().AvatarIndex.Value = x;
            playerobj.GetComponent<NetworkObject>().SpawnWithOwnership(client.ClientId, true);

        }


    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

    }




    // public void DoOperation()
    // {

    //     parentref = Instantiate(emptyobj);
    //     objref = Instantiate(GraphicsCharacter, parentref.transform);
    //     ResetAnimator();

    // }

    // public void SwapCharacter()
    // {

    //     Destroy(objref);
    //     objref = Instantiate(NextCharacter, parentref.transform);
    //     objref.transform.SetAsFirstSibling();
    //     ResetAnimator();

    //     // StartCoroutine(Mext());


    // }

    // void ResetAnimator()
    // {
    //     parentref.GetComponent<Animator>().Rebind();
    //     parentref.GetComponent<Animator>().Update(0);

    // }

    // public void SwapController()
    // {

    //     parentref.GetComponent<Animator>().runtimeAnimatorController = overrideController;

    // }
}

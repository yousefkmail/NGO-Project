using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkManagerListener : MonoBehaviour
{



    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void JoinHost()
    {
        NetworkManager.Singleton.StartClient();
    }
}

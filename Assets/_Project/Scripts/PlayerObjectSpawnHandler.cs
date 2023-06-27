using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerObjectSpawnHandler : NetworkBehaviour
{

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsServer)
        {
            this.GetComponent<CharacterController>().enabled = true;
            GetComponent<PlayerControllerState>().enabled = true;
            GetComponent<ServerAnimator>().enabled = true;
        }
        if (IsOwner)
        {
            GetComponent<ClientInputSender>().enabled = true;

        }
    }

}

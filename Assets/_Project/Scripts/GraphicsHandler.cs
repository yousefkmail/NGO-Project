using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GraphicsHandler : NetworkBehaviour
{

    public NetworkVariable<int> AvatarIndex = new NetworkVariable<int>();
    [SerializeField]
    public PlayerCharacter[] graphicss;


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        Instantiate(graphicss[AvatarIndex.Value].AvatarGraphics, this.transform);
        GetComponent<Animator>().Rebind();
        GetComponent<Animator>().Update(0f);

    }




}

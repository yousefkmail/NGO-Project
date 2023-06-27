using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CharacterControllerGroundedState : NetworkBehaviour
{
    public NetworkVariable<bool> IsGrounded = new NetworkVariable<bool>(false);
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (IsServer)
            IsGrounded.Value = GetComponent<CharacterController>().isGrounded;
    }
}

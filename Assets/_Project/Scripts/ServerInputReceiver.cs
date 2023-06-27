using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
[RequireComponent(typeof(PlayerInput))]
public class ServerInputReceiver : NetworkBehaviour
{
    [HideInInspector]
    public Vector2 MoveValue = new Vector2(0, 0);

    [HideInInspector]
    public bool Jump = false;
    [HideInInspector]
    public float CameraRotationY;



    [ServerRpc]
    public void SetJumpValueServerRPC(bool value)
    {

        Jump = value;

    }


    [ServerRpc]
    public void SetMoveDirectionServerRPC(Vector2 value)
    {

        MoveValue = value;

    }

    [ServerRpc]
    public void SetCameraRotationYServerRPC(float y)
    {

        CameraRotationY = y;

    }

}

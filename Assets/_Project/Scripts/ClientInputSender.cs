using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class ClientInputSender : MonoBehaviour
{
    [SerializeField] ServerInputReceiver serverInputReceiver;
    PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }


    void Update()
    {

        serverInputReceiver.SetJumpValueServerRPC(playerInput.actions["Fire"].triggered);
        serverInputReceiver.SetMoveDirectionServerRPC(playerInput.actions["Move"].ReadValue<Vector2>());
        serverInputReceiver.SetCameraRotationYServerRPC(Camera.main.transform.eulerAngles.y);
    }
}

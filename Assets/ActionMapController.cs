using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ActionMapController : MonoBehaviour
{
    [SerializeField] string actionMapName = "Player";
    [SerializeField] InputActionAsset inputActions;
    public void EnablePlayerControls()
    {
        inputActions.FindActionMap(actionMapName).Enable();
    }

    public void DisablePlayerControls()
    {
        inputActions.FindActionMap(actionMapName).Disable();
    }
}

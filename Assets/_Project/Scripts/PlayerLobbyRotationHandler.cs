using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLobbyRotationHandler : MonoBehaviour
{
    [SerializeField] float RotationSpeed = 1;
    
     Vector3 CurrentMouseWorldPosition;

    void OnMouseDown()
    {
        CurrentMouseWorldPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);


    }


    void OnMouseDrag()
    {

        var currentMouseWorldPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        this.transform.Rotate(0, (CurrentMouseWorldPosition.x - currentMouseWorldPosition.x) * 360 * RotationSpeed, 0);
        CurrentMouseWorldPosition = currentMouseWorldPosition;

    }
}

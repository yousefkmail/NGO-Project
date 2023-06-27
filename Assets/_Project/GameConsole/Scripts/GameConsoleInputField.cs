using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class GameConsoleInputField : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    // Start is called before the first frame update

    public static event Action<string> OnMessageSent;
    void Start()
    {

    }

    void Update()
    {
        EnterKeyOnTextField();
    }

    private void EnterKeyOnTextField()
    {
        if (!Input.GetKeyDown(KeyCode.Return))
        {
            return;
        }
        if (EventSystem.current.currentSelectedGameObject == inputField.gameObject)
        {
            EventSystem.current.SetSelectedGameObject(null);
            SubmitTextToVivox();
            // GetComponent<ActionMapController>().EnablePlayerControls();

        }
        else
        {
            EventSystem.current.SetSelectedGameObject(inputField.gameObject);
            // GetComponent<ActionMapController>().DisablePlayerControls();
        }

    }

    private void SubmitTextToVivox()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            return;
        }
        OnMessageSent?.Invoke(inputField.text);
        inputField.text = "";
    }



}

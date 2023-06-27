using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessagePopUpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Button button;
    public event Action OnbuttonClicked;

    public void InitPopUp(string message, string button)
    {
        this.message.text = message;
        this.buttonText.text = button;

    }

    public void OnButtonClicked()
    {
        Destroy(this.gameObject);
        OnbuttonClicked?.Invoke();
    }
}

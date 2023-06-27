using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPopUpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;

    public void InitPopUp(string message)
    {
        this.message.text = message;

    }

    public void Close()
    {
        Destroy(this.gameObject);
    }

}

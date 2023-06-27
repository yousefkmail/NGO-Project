using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField] LoadingPopUpUI popUpUI;
    [SerializeField] MessagePopUpUI MessagePopUpUI;
    [SerializeField] Transform Panel;

    public static PopUpManager Instance;



    public static LoadingPopUpUI CreateLoadingPopUpUI(string content)
    {
        if (Instance != null)
        {
            LoadingPopUpUI popUpObject = Instantiate(Instance.popUpUI, Instance.Panel);
            popUpObject.InitPopUp(content);
            return popUpObject;

        }
        else
        {
            Debug.LogError("Couldn't find Popup manager singleton, All pop up creating will return null ");
            return null;
        }
    }


    public static MessagePopUpUI CreateMessagePopUpUI(string content, string buttonText)
    {
        if (Instance != null)
        {
            MessagePopUpUI popUpObject = Instantiate(Instance.MessagePopUpUI, Instance.Panel);
            popUpObject.InitPopUp(content, buttonText);
            return popUpObject;

        }
        else
        {
            Debug.LogError("Couldn't find Popup manager singleton, All pop up creating will return null ");
            return null;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {

            Debug.LogError("Multiple Popup managers detected,Destroying the newest one ");
            Destroy(this.gameObject);
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RelayCodeDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI RelayCode;
    // Start is called before the first frame update
    void Start()
    {
        RelayCode.text = PlayerPrefs.GetString("RelayCode");
    }


}

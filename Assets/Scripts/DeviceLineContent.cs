using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeviceLineContent : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;

    private void Start() {
        
    }

    public void setText(string deviceName, string deviceLineContent){
        textField.text = deviceName+deviceLineContent;
    }
}

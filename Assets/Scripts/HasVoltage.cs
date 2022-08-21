using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class HasVoltage : MonoBehaviour,Interact_Interface
{
    protected float voltage;
    public GameObject voltageLabel;
    public TextMeshProUGUI voltageCheckResult;

    public void interact(){
        voltageLabel.SetActive(true);
        voltageCheckResult.text = voltage.ToString();
    }

    public GameObject getLabel()
    {
        return voltageLabel;
    }

}

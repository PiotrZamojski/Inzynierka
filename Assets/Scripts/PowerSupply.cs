using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupply : HasVoltage
{

    public float Voltage;
    // Start is called before the first frame update
    void Start()
    {
        voltage = Voltage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputPowerFirstAmp : HasVoltage
{
    public ResistorSlot slot1, slot2;
    public PowerSupply powerSupply;

    double voltageResult;
    double resistanceAmp;

    public double Voltage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        resistanceAmp = slot1.resistance/slot2.resistance;
        voltageResult = resistanceAmp * powerSupply.Voltage;
        Voltage = voltageResult;
        voltage = voltageResult;
    }
}

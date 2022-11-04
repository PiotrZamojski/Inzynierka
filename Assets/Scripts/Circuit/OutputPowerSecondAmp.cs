using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputPowerSecondAmp : HasVoltage
{
    public ResistorSlot slot1, slot2, slot3, slot4;
    public PowerSupply powerSupply1, powerSupply2, powerSupply3;

    double voltageResult;
    double voltateFirstAmpResult, voltateSecondAmpResult, voltateThirdAmpResult;
    double resistanceFirstAmp, resistanceSecondAmp, resistanceThirdAmp;

    public double Voltage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        resistanceFirstAmp = slot4.resistance/slot1.resistance;
        resistanceSecondAmp = slot4.resistance/slot2.resistance;
        resistanceThirdAmp = slot4.resistance/slot3.resistance;

        voltateFirstAmpResult = resistanceFirstAmp * powerSupply1.Voltage;
        voltateSecondAmpResult = resistanceSecondAmp * powerSupply2.Voltage;
        voltateThirdAmpResult = resistanceThirdAmp * powerSupply3.Voltage;
        voltageResult = voltateFirstAmpResult + voltateSecondAmpResult + voltateThirdAmpResult;
        Voltage = voltageResult;
        voltage = voltageResult;
    }
}

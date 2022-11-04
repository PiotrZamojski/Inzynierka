using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputPower : HasVoltage
{
    public ResistorSlot slot1, slot2, slot3, slot4, slot5;
    public PowerSupply powerSupply;

    double voltageResult;
    double resistanceSeries;
    double resitanceParallel;
    double resistanceEquivalent;
    double current;
    double voltageResistor1;
    double voltageResistor2;
    double currentThroughResistor2;
    double currentThroughResistor345;

    public double Voltage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        resistanceSeries = slot3.resistance + slot4.resistance + slot5.resistance;
        resitanceParallel = ((resistanceSeries*slot2.resistance)/(resistanceSeries+slot2.resistance));
        resistanceEquivalent = slot1.resistance + resitanceParallel;

        current = powerSupply.Voltage / resistanceEquivalent;

        voltageResistor1 = slot1.resistance * current;
        voltageResistor2 = powerSupply.Voltage - voltageResistor1;

        currentThroughResistor2 = voltageResistor2 / slot2.resistance;
        currentThroughResistor345 = current - currentThroughResistor2;

        voltageResult = currentThroughResistor345 * slot5.resistance;
        Voltage = voltageResult;
        voltage = voltageResult;
    }
}

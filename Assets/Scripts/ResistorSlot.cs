using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistorSlot : MonoBehaviour,Interact_Interface
{
    private float resistance;
    public GameObject resistorSlot;
    private bool interacting = false;
    public void interact(){
        if (!interacting)
           print("Resistor here!");
    }
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
}

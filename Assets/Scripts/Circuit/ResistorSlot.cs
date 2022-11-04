using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistorSlot : MonoBehaviour,Interact_Interface
{
    public PlayerMovement player;
    public double resistance = 1.0;
    private GameObject resistorObj;
    public LayerMask mask;

    public void interact(){ 
       GameObject item = player.getItem();


//TODO 
//nagle rezystor slot przechowuje rezystancje caly czas
//do poprawy

       if(item == null){ 
            print("give item");
            player.giveItem(resistorObj);
            resistorObj = null;
            resistance = 1.0;
       }
       else if(item.GetComponent<Resistor>()){
            Rigidbody resistor = item.GetComponent<Rigidbody>();
            resistor.useGravity = true;
            resistor.drag = 1;
            resistor.constraints = RigidbodyConstraints.None;
            item.transform.parent = null;
            item.transform.SetParent(gameObject.transform,true); 
            resistor.useGravity = false;
            resistor.drag = 10;
            resistor.transform.position = gameObject.transform.position;
            resistor.transform.rotation = gameObject.transform.rotation;
            resistor.constraints = RigidbodyConstraints.FreezeRotation;
            resistorObj = item;
            item.layer = LayerMask.NameToLayer("Interactable");
            resistance = resistorObj.GetComponent<Resistor>().getResistanceValue();
       }
       
    }

    public double getResistance(){
        return resistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 0){
            resistorObj = null;
            resistance = 1.0;
        }
    }
}

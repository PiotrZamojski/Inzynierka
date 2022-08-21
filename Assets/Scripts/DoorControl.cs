using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour,Interact_Interface
{
    public GameObject door;
    public OutputPower outputPower;
    [SerializeField] double referenceVoltage;
    Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = door.GetComponent<Animator>();
        print(doorAnimator);
    }


    public void interact(){
        print((float)outputPower.Voltage - (float)referenceVoltage);
        if(Mathf.Abs((float)outputPower.Voltage - (float)referenceVoltage) < 0.001f){
            print("Open the door");
            doorAnimator.SetBool("voltageCorrect",true);
            gameObject.transform.GetComponent<Renderer>().material.color = Color.green;
        }
        else{
            doorAnimator.SetBool("voltageCorrect",false);
            gameObject.transform.GetComponent<Renderer>().material.color = Color.red;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

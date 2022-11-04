using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resistor : MonoBehaviour,Interact_Interface
{
    [SerializeField] double resistance;
    public PlayerMovement player;

    public double getResistanceValue(){
        return resistance;
    }

    public void interact(){
        player.pick(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

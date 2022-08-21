using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public GameObject checkHit(float handRange)
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, fwd,out hit,handRange,LayerMask.GetMask("Interactable")) || Physics.Raycast(transform.position, fwd,out hit,handRange+10,LayerMask.GetMask("Slot"))){
            Debug.DrawRay(transform.position, fwd * hit.distance, Color.yellow);
            if(hit.collider.gameObject.GetComponent<Interact_Interface>() != null){
                print("Something i can interact with");
            }
            
            try{
                return hit.collider.gameObject;
            }catch(System.Exception e){
                print("Cannot get instance");
            }
        }
        return null;
    }

}

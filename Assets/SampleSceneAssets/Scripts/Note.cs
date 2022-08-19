
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Note : MonoBehaviour
{
    [SerializeField]
    private Image _noteImage;
    
    void onTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            _noteImage.enabled = true;      
        }
    }

    void onTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            _noteImage.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Document : MonoBehaviour,Interact_Interface
{
    public Sprite _noteImage;
    public GameObject _showImage;
    private bool interacting = false;

    public void interact(){
        if(!interacting){
            print("Show Image");
            _showImage.GetComponent<Image>().enabled = true;
            _showImage.GetComponent<Image>().sprite = _noteImage;
            interacting = true;
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Hide Message") ){
            interacting = false;
            _showImage.GetComponent<Image>().enabled = false;
        }
    }
}

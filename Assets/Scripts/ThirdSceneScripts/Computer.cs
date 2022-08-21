using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour,Interact_Interface
{

    public GameObject _showDesktop;
    public GameObject _mainCanvas;
    private bool interacting = false;
    public PlayerMovement _player;

    public void interact(){
        if(!interacting){
            _player.enabled = false;
            print("Show Image");
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _showDesktop.SetActive(true);
            _mainCanvas.SetActive(false);
            Time.timeScale = 0f;
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
        if(interacting){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(Input.GetKeyDown("p") ){
            interacting = false;
            _player.enabled = true;
             _showDesktop.SetActive(false);
             _mainCanvas.SetActive(true);
            Time.timeScale = 1f;
            Cursor.visible = false;

        }
    }
}

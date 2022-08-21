using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    public void setActiveWindow(GameObject activeWindow){
        GameObject[] terminals = GameObject.FindGameObjectsWithTag("Terminal");
        foreach (var terminal in terminals)
        {
            terminal.SetActive(false);
        }
        activeWindow.SetActive(true);
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

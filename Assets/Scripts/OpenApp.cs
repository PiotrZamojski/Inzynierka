using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenApp : MonoBehaviour
{
    public GameObject app;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(){
        app.SetActive(true);
    }
}

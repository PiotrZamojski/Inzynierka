using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControl : MonoBehaviour
{
    private Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        myLight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("FlashLightOnOFF")){
            myLight.enabled = !myLight.enabled;
        }
    }
}

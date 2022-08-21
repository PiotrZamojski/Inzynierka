using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SimulateCommandLine : MonoBehaviour
{
    TextMeshProUGUI line;

    private bool inProgress = false; 
    // Start is called before the first frame update
    void Start()
    {
        line = transform.GetComponent<TextMeshProUGUI>();
        line.text = "Switch>";
    }

    // Update is called once per frame
    void Update()
    {
        if(!inProgress){
            StartCoroutine(Blink());
        }
        
    }

    IEnumerator Blink(){
        inProgress = true;
        if(line.text == "Switch>"){
            line.text = "Switch>|";
        }
        else{
            line.text = "Switch>";
        }
        yield return new WaitForSeconds(1);
        inProgress = false;
    }
}

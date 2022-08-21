using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class TerminalManager : MonoBehaviour
{
    public GameObject deviceLine;
    public GameObject responseLine;
    
    public TMP_InputField userInput;
    public GameObject userInputLine;
    public ScrollRect sr;
    public GameObject msgList;

    
    private string state = "loggedOut";
    private bool waitForCommands = true;
    private Interpreter interpreter;

    private Vector2 originalSizeOfUserInput;
    private Vector2 originalSizeOfDeviceLine;

    //Data from network device
    public string name;
    public string deviceBegin;

    
    public void getData(string deviceName){
        name = deviceName;
        updateLines();
    }

    private void Start() {
        interpreter = GetComponent<Interpreter>();
        originalSizeOfUserInput = userInputLine.GetComponentsInChildren<RectTransform>()[1].sizeDelta;
        print(originalSizeOfUserInput); 
    }

    private void Update() {
        
    }

    private void OnGUI(){
         
        if(Input.GetKeyUp(KeyCode.Return)){
            waitForCommands = true;
        }
        if(waitForCommands){
             if(userInput.isFocused && String.Equals(state,"loggedOut") && Input.GetKeyDown(KeyCode.Return)){
            string userInputText = userInput.text;
            print(userInputText);
            ClearInput();

            int lines = interpretingLines(interpreter.Interprete(userInputText));

            state = interpreter.getState();
            print("state in terminal: "+state);

            ScrollToTheBottom(lines);

            userInputLine.transform.SetAsLastSibling();
            userInput.ActivateInputField();
            userInput.Select();
            updateLines();
            waitForCommands = false;
        }
        else if(userInput.isFocused && userInput.text != "" && Input.GetKeyDown(KeyCode.Return)){

            //store input
            string userInputText = userInput.text;

            //Clear
            ClearInput();

            AddDeviceLine(userInputText);

            int lines = interpretingLines(interpreter.Interprete(userInputText));

            state = interpreter.getState();
            
            print("state in terminal: "+state);

            ScrollToTheBottom(lines);

            userInputLine.transform.SetAsLastSibling();

            userInput.ActivateInputField();
            userInput.Select();
            updateLines();
            waitForCommands = false;
        }
        else if(userInput.isFocused && userInput.text != "" && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.C) && !String.Equals(state,"loggedOut")){
            string userInputText = userInput.text;
            ClearInput();

            AddDeviceLine(userInputText);
            ScrollToTheBottom(1);

            userInputLine.transform.SetAsLastSibling();

            userInput.ActivateInputField();
            userInput.Select();
            waitForCommands = false;

        }
        }
       
       
        
    }

    void ClearInput(){
        userInput.text = "";
    }

    bool descale = false;

    private void updateLines(){
            if(String.Equals(state,"loggedOut")){
                print("should change line to press enter");
                userInputLine.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "Press enter...";
                Vector2 position = userInput.gameObject.GetComponent<RectTransform>().anchoredPosition;
                if(position.x < 160){
                    scaleLines(172);
                }
                
                descale = true;
            }
            else if(String.Equals(state,"normal")){
                print("should change line to normal");
                userInputLine.GetComponentsInChildren<TextMeshProUGUI>()[0].text = name+">";
                deviceBegin = name+">";
                if(descale){
                    descale=false;
                    scaleLines(-172);
                }
            }else if(String.Equals(state,"enabled")){            
                userInputLine.GetComponentsInChildren<TextMeshProUGUI>()[0].text = name+"#";              
                deviceBegin = name+"#";
                if(descale){
                    descale=false;
                    scaleLines(-172);
                }
            }
            else if(String.Equals(state,"configuration terminal")){
                userInputLine.GetComponentsInChildren<TextMeshProUGUI>()[0].text = name+"(config)#";            
                deviceBegin = name+"(config)#";   
                //scale lines
                if(descale == false){
                    scaleLines(172);
                    descale = true;
                }
                
                               
            }
            print("state after updating lines: "+state);
        
    }

    void scaleLines(int scale){
        //przeprogramować tak by zmiana zachodziła dynamicznie a nie na podstawie argumentu scale
        //pojawiaja sie problemy przy hostname ponieważ nie wiadomo jaką długość będzie miała podana przez użytkownika nazwa
        print("scale lines");
        Vector2 position = userInput.gameObject.GetComponent<RectTransform>().anchoredPosition;
        
        //Vector2 sizeOfInputLine = userInputLine.GetComponentsInChildren<RectTransform>()[0].sizeDelta;
        //print("difference: "+(sizeOfInputLine.x - originalSizeOfUserInput.x));
        userInput.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x + scale,position.y);
        
       // originalSizeOfUserInput = sizeOfInputLine;
    }


    void AddDeviceLine(string userInputText){
        //Resizing the command line container.
        resize();
        //Instantine the device line
        GameObject msg = Instantiate(deviceLine,msgList.transform);

        //Set child index
        msg.transform.SetSiblingIndex(msgList.transform.childCount -1);


        //Set the text of this new gameobject
        DeviceLineContent deviceContent =  msg.GetComponent<DeviceLineContent>();

        deviceContent.setText(deviceBegin,userInputText);
    }

    int interpretingLines(List<string> interpretation){
        for(int i = 0;i < interpretation.Count;i++){

            GameObject response = Instantiate(responseLine,msgList.transform,true);
            response.transform.SetAsLastSibling();

            resize();

            response.GetComponentsInChildren<TextMeshProUGUI>()[0].text = interpretation[i];        
        }
        return interpretation.Count;
    }

    void resize(){
        Vector2 msgListSize = msgList.GetComponent<RectTransform>().sizeDelta;
        msgList.GetComponent<RectTransform>().sizeDelta = new Vector2(msgListSize.x,msgListSize.y + 60.0f);
    }

    void ScrollToTheBottom(int lines){

        if(lines > 40){
            sr.velocity = new Vector2(0,450);
        }
        else{
            sr.verticalNormalizedPosition = 0;
        }
    }
}

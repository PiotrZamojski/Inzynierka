using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour , NetworkDevice
{
    
    [SerializeField] string name;
    [SerializeField] string type;
    [SerializeField] GameObject terminal;
    [SerializeField] WindowManager windowManager;

    //private variables
    private string enabledModePassword = "";
    private string userModePassword = "";
    private bool needUserPassword = false;
    private bool needEnPassword = false;


    public void setName(string hostname){
        name = hostname;
        terminal.GetComponent<TerminalManager>().getData(name);
    }

    public string getName(){
        return name;
    }

    public string getType(){
        return type;
    }

    public void openTerminal(){
        windowManager.setActiveWindow(terminal);
        terminal.GetComponent<TerminalManager>().getData(name);
    }

    public void setPasswordEn(string password){
        enabledModePassword = password;
    }

    public void setPasswordUser(string password){
        userModePassword = password;
    }

    public void needPassword(){
        needUserPassword = true;
    }
    public bool getIfPasswUserNeeded(){
        return needUserPassword;
    }

    public string getEnPasswd(){
        return enabledModePassword;
    }

    public string getUserPasswd(){
        return userModePassword;
    }
   

    public void needEnabPassword(){
        needEnPassword = true;
    }
    public bool getIfPasswEnNeeded(){
        return needEnPassword;
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

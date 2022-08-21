using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Interpreter : MonoBehaviour
{
    [SerializeField] GameObject device; //pozwala na dokonywanie zmian na konfiguracji urządzenia sieciowego
    private NetworkDevice myDevice;

    private bool enablePassAwait = false;
    private bool loggedEn = false;
    private bool loggedOut = true;
    private bool wait = false;
    private static int i = 0;

    Dictionary<string,System.Func<string[],string>> instructions;

    private string state = "loggedOut"; 

    Dictionary<string,System.Func<string[],string>> loggedOutMode;

    Dictionary<string,System.Func<string[],string>> normalMode;

    Dictionary<string,System.Func<string[],string>> enabledMode;

    Dictionary<string,System.Func<string[],string>> configuratingMode; 
    List<string> terminalResponse = new List<string>();

    Dictionary<string,Dictionary<string,System.Func<string[],string>>> stateDict;


    //Sprawdzanie jaki jest stan i zwrocenie odpowiedniego slownika z instrukcjami
    private Dictionary<string,System.Func<string[],string>> checkState(){
        return stateDict[state];
    }

    public List<string> Interprete(string userInput){
        terminalResponse.Clear();
        string[] input = userInput.Split();
        if(loggedOut){
            return checkLoginCredentials(input);
        }
        
  
        string[] output;


        //sprawdzamy czy nie czekamy na podanie hasla,jesli tak to przeprowadzamy probe logowania użytkownika
        //jak się powiedzie to ustawiamy odpowiedni stan i informacje zwrotne
        
        //dla trybu uprzywilejowanego
        if(enablePassAwait && String.Equals(input[0],myDevice.getEnPasswd())){
            enablePassAwait = false;
            loggedEn = true;
            state = "enabled";
            terminalResponse.Add("");
            return terminalResponse;
        }else if(enablePassAwait){
            terminalResponse.Add("Wrong password");
            return terminalResponse;
        }


        //odczytywanie instrukcji i dopasowywanie im odpowiednich funkcji
        try{
            output = instructions[input[0]](input).Split("\n");
        }catch(KeyNotFoundException e){
            print("here somehow");
            terminalResponse.Add("Invalid command");
            return terminalResponse;
        }
        

        foreach (string text in output)
        {
            terminalResponse.Add(text);
        }
        
        return terminalResponse;

    }

    //Sprawdzenie hasla
    private List<string> checkLoginCredentials(string[] input){
        if(myDevice.getIfPasswUserNeeded()){
            print("password needed");
            if(String.Equals(input[0],myDevice.getUserPasswd())){
                state = "normal";
                loggedOut = false;
                terminalResponse.Add("");
                return terminalResponse;
            }else{
                terminalResponse.Add("Invalid password");
                return terminalResponse;
            }
        }else{
            print("zmieniam na normal tutaj");
            state = "normal";
            loggedOut = false;
            terminalResponse.Add("");
            return terminalResponse;
        }
    }
    //=======================================================================================================================================================




    //Wychodzenie z danego stanu=======================================================================================================================
    private string Exit(string[] toState){
        state = toState[0];
        if(String.Equals(state,"loggedOut")){
            loggedOut = true;
            wait = true;
            print("log out");
        }
        if(String.Equals(state,"normal")){
            loggedEn = false;
        }
        print("State: "+state);
        return "Exit mode";
    }
    //=================================================================================================================================================

    public string getState(){
        return state;
    }





    //Help methods ================================================================================================================================
    private string Help(string[] input){
        return "Exec commands:\n"+"connect\tOpen a terminal connection\ndisable\tTurn off privileged commands\ndisconnect\tDisconnect an existing network connection"+
        "\nshow\tShow running system information\nenable\tTurn on privileged mode";
    }

    private string HelpEn(string[] input){
        return "Enabled mode help";
    }

    private string HelpConf(string[] input){
        return "Configuration mode help";
    }
    //==================================================================================================================================================


    
    //Echo methods=====================================================================================================================================
    private string Echo(string[] input){
        string[] text = input.ToList().Skip(1).ToArray();
        print("echo");
        return string.Join(" ",text);
    }

    private string EchoEn(string[] input){
        string[] text = input.ToList().Skip(1).ToArray();
        print("echo");
        return "Enabled mode echo: "+string.Join(" ",text);
    }
    //====================================================================================================================================================


    //***************************************************************METODY WEJSCIA DO KONKRETNYCH TRYBOW**********************************************************************

    //Enable method=======================================================================================================================================
    private string Enable(string[] input){
        if(myDevice.getIfPasswEnNeeded() && !loggedEn){
                // Dodać obługę wczytywania hasła
                //można zwrócić na ekran komunikat o konieczności podania hasła wtedy ustawić flagę interpretera na oczekiwanie na hasło
                enablePassAwait = true;
                return "enter password:";
        }else{
             state = "enabled";
             return "OK";
        }
       
    }
    //======================================================================================================================================================



    //Configuration method===================================================================================================================================
    private string Configuration(string[] input){
        try{
            if(input[1] != null){
                if(input[1] ==  "terminal"){
                    state = "configuration terminal";
                    return "Enter configuration commands,one per line.";
                }
                else{
                    return "Invalid input: "+input[1];
                }
            }
        }
        catch(System.Exception e){
            state = "configuration terminal";
            return "Enter configuration commands,one per line.";
        }
        return "something";
    }
    //==========================================================================================================================================================


    //***************************************************************************************************************************************************************************



    //Metoda pozwalajaca na zmiane nazwy hosta====================================================================================================================
    private string Hostname(string[] input){
        try{
            this.transform.parent.GetComponent<NetworkDevice>().setName(input[1]);
        }catch(System.Exception e){
            print(e);
            return "not ok";
        }
        
        return "ok";
    }
    //============================================================================================================================================================


    //**********************************************************************METODY DO STEROWANIA HASLAMI W URZADZENIU**********************************************


    //Ustawienie hasla w trybie uprzywilejowanym=====================================================================================================================
    private string SetPasswordEn(string[] input){
        try{
            myDevice.setPasswordEn(input[2]);
        }catch(System.Exception e){
            print(e);
            return "not ok";
        }
         return "ok";
    }
    //==================================================================================================================================================================



    //Ustawienie hasla dla trybu uzytkownika============================================================================================================================
    private string SetPasswordUser(string[] input){
        try{
            myDevice.setPasswordUser(input[1]);
        }catch(System.Exception e){
            print(e);
            return "not ok";
        }
        return "ok";
    }
    //=================================================================================================================================================================



    //Funkcje ustawiajace wymaganie hasla======================================================================================================================
    //dla trybu uprzywilejowanego
    private string NeedPasswordEn(string[] input){
        myDevice.needEnabPassword();
        return "ok";
    }


    //dla trybu uzytkownika
    private string NeedPasswordUser(string[] input){
        myDevice.needPassword();
        return "ok";
    }
    //==================================================================================================================================================================


    //Funkcja ktora jest uzupelnieniem do komendy enable,sprawdzamy czy uzytkownik wpisal enable secret [haslo]=======================================================
    private string EnablePassword(string[] input){
        if(String.Equals(input[1],"secret")){
            SetPasswordEn(input);
            NeedPasswordEn(input);
            return "ok";
        }
        else{
            return "invalid operation";
        }
    }
    //=================================================================================================================================================================



    // private string ExitNormal(string[] input){
    //     state = "loggedOut";
    //     return "logged out";
    // }

    // Start is called before the first frame update
    void Start()
    {
        try{
            myDevice = device.GetComponent<NetworkDevice>();
        }catch(System.Exception e){
            print(e);
        }

        loggedOutMode = new Dictionary<string,System.Func<string[],string>>();

        normalMode  = new Dictionary<string,System.Func<string[],string>>()
        {
        {"?", (input) => Help(input)},
        {"echo", (input) => Echo(input)},
        {"enable",(input) => Enable(input)},
        {"exit",(input) => Exit(new[]{"loggedOut"})},
        };

        enabledMode  = new Dictionary<string,System.Func<string[],string>>()
        {
        {"?", (input) => HelpEn(input)},
        {"echo", (input) => EchoEn(input)},
        {"exit", (input) => Exit(new []{"normal"})},
        {"configuration",(input) => Configuration(input)},
        };

        configuratingMode = new Dictionary<string,System.Func<string[],string>>()
        {
        {"?", (input) => HelpConf(input)},
        {"exit", (input) => Exit(new []{"enabled"})},
        {"hostname", (input) => Hostname(input)},
        {"password",(input) => SetPasswordUser(input)},
        {"login",(input) => NeedPasswordUser(input)},
        {"enable",(input) => EnablePassword(input)},
        };


        stateDict = new Dictionary<string,Dictionary<string,System.Func<string[],string>>>(){
        {"loggedOut",loggedOutMode},
        {"normal",normalMode},
        {"enabled",enabledMode},
        {"configuration terminal",configuratingMode},
        };



    }

    // Update is called once per frame
    void Update()
    {
        instructions = checkState();
    }
}

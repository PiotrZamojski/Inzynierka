using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NetworkDevice
{
    public string getName();
    public void setName(string hostname);
    public string getType();
    public void openTerminal();
    public void setPasswordEn(string password);
    public void setPasswordUser(string password);
    public void needPassword();
    public bool getIfPasswUserNeeded();
    public string getEnPasswd();
    public string getUserPasswd();
    public void needEnabPassword();
    public bool getIfPasswEnNeeded();

}

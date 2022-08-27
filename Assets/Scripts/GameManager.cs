using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool gameEnd = false;
    
    public void CompleteLevel(){
        Debug.Log("Level won");
    }
    public void EndGame(){
        if(gameEnd != false){
            gameEnd = true;
            Debug.Log("Game Over");
        }
    }
}

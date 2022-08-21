using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public OutputPower outputPower = null;
    public Fader fader;

    [SerializeField] int sceneToLoad = -1;
    public GameObject spawnPoint;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            if(outputPower != null && outputPower.Voltage == 1){
                print("starting coroutine");
                StartCoroutine(Transition());
            }
            else if(outputPower == null){
                print("starting coroutine");
                StartCoroutine(Transition());
            }
            print("starting coroutine");
            StartCoroutine(Transition());
            
        }
    }

    private IEnumerator Transition(){
        DontDestroyOnLoad(gameObject);

        yield return fader.FadeOut(3f);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        Teleport teleport = GetNewTeleport();
        PlayerStartPosition(teleport);

        print("loaded");
        yield return fader.FadeIn(2f);
        Destroy(gameObject);
    }

    private void PlayerStartPosition(Teleport teleport){
        GameObject player =  GameObject.FindWithTag("Player");
        player.transform.position = teleport.spawnPoint.transform.position;
        player.transform.rotation = teleport.spawnPoint.transform.rotation;
    }

    private Teleport GetNewTeleport(){
        foreach (Teleport teleport in FindObjectsOfType<Teleport>())
        {
            if(teleport == this) continue;

            return teleport;
        }

        return null;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{

    //resets the scene after the random time 
    private float time = 0.0f; 

    public float timeMin;
    public float timeMax; 
    public float randomTime;

    void Start(){
        randomTime = Random.Range(timeMin, timeMax);
    }
 
    void Update()
    {
        time += Time.deltaTime; 
        if(time >= randomTime){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

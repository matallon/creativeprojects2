using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterMovement : MonoBehaviour
{   
    //moves the plane that draws the water slightly up and down to give it some slight movement 
    float counter = 0; 
    // Update is called once per frame
    void Update()
    {
        counter +=0.5f; 
        if(counter%30==0){
            transform.position = new Vector3(0f,45 + (Mathf.Cos(counter) * 0.09f), 0f);
        }
    }
}

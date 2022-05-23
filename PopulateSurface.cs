using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateSurface : MonoBehaviour
{
    //all the game objects being instantiated 
    public GameObject cloud1;
    public GameObject cloud2;
    public GameObject cloud3;
    public GameObject cloud4;
    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3; 
    public GameObject flower; 
    public GameObject beachObject; 
    public GameObject person; 

    public int numberOfObjects;
    public int currentObjects;

    private float randomX;
    private float randomZ;
    private Renderer r;

    private int peopleInSpot; 
    private float heightOffGround;

    public int cloudsAmount; 
    int cloudCount; 
 
    void Start() {
        //for the raycasting 
        r = GetComponent<Renderer>();

        //get random amounts  
        peopleInSpot = Random.Range(0,10);
        heightOffGround = Random.Range(0,100);
        cloudsAmount = Random.Range(5,20); 
        cloudCount = 0; 
    }
 
     void Update() {
         //spawn the clouds
         if(cloudCount < cloudsAmount){
            int randomCloud = Random.Range(0,4);
            if(randomCloud == 1){
                Vector3 cloudPoint = new Vector3(Random.Range(-1000,1000),Random.Range(520, 590),Random.Range(-1000,1000));
                Instantiate(cloud1, cloudPoint, Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                cloudCount += 1; 
            } else if(randomCloud == 2){
                Vector3 cloudPoint = new Vector3(Random.Range(-1000,1000),Random.Range(520, 590),Random.Range(-1000,1000));
                Instantiate(cloud2, cloudPoint, Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                cloudCount += 1; 
            } else if(randomCloud == 3){
                Vector3 cloudPoint = new Vector3(Random.Range(-1000,1000),Random.Range(520, 590),Random.Range(-1000,1000));
                Instantiate(cloud3, cloudPoint, Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                cloudCount += 1; 
            } else if(randomCloud == 4){
                Vector3 cloudPoint = new Vector3(Random.Range(-1000,1000),Random.Range(520, 590),Random.Range(-1000,1000));
                Instantiate(cloud4, cloudPoint, Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                cloudCount += 1; 
            }
         }

         RaycastHit hit;
         if(currentObjects <= numberOfObjects) { 
            randomX = Random.Range(r.bounds.min.x, r.bounds.max.x);
            randomZ = Random.Range(r.bounds.min.z, r.bounds.max.z);

            int waterPoint = 50; 
            int highestPoint = 230;

            //spawn all the other objects using a raycast which hits the mesh to find their random position. 

             if (Physics.Raycast(new Vector3(randomX, r.bounds.max.y + 5f, randomZ), -Vector3.up, out hit)) {
                 //spawn all the new objects
                float yPoint = hit.point.y;
                 if(currentObjects < 50  &&  yPoint > waterPoint &&  yPoint < highestPoint){  //so they don't spawn in the water
                     //first tree 
                     Instantiate(tree1, hit.point, Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                 } else if(currentObjects < 100 &&  yPoint > waterPoint &&  yPoint < highestPoint){ //so they don't spawn in the water 
                    //second tree
                     Instantiate(tree2, hit.point,  Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                 } else if(currentObjects < 125 &&  yPoint > waterPoint &&  yPoint < highestPoint){
                     Instantiate(tree3, hit.point,  Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                 } else if(currentObjects < 150 &&  yPoint > waterPoint&&  yPoint < highestPoint){
                     Instantiate(flower, hit.point,  Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                 } else if(currentObjects < 200 &&  yPoint < waterPoint &&  yPoint > waterPoint - 10){
                     Instantiate(beachObject, hit.point,  Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                     Instantiate(beachObject, hit.point,  Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                 } else if(currentObjects < 220 &&  yPoint > waterPoint &&  yPoint < highestPoint){
                     for(int i = 0; i < peopleInSpot; i++){
                        Vector3 aboveSurface = new Vector3(hit.point.x, hit.point.y + heightOffGround, hit.point.z);
                        Instantiate(person, aboveSurface,  Quaternion.Euler(Vector3.up * Random.Range(0,360)));
                     }
                 }
                 currentObjects += 1;
             }
         }
     }
}

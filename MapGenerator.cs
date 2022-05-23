using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based off procedural generation tutorial series by Sebastian Lague referenced in my Creative Projects 2 document.
//Has obviously been altered to make it slightly more my own but is based upon some of those tutorials. 

public class MapGenerator : MonoBehaviour {
	//variables to create the noise with 
	int mapWidth = 200;
	int mapHeight = 200;
	int seed = 0;
	
	//control the curves of the numbers, limit the heights to a certain curve. 
	public AnimationCurve meshHeightCurve; 
	
	//regions in the inspector where colours are assigned for the different height values. 
	public TerrainType[] regions; 

	public void GenerateMap() {
		//call the generate noise map function defined in a different script
		float[,] noiseMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, seed, 81.0f, 5, 0.54f, 1.64f);

		Color[] colourMap = new Color[mapWidth * mapHeight];

		//set the colours of the regions 
		for(int y = 0; y < mapHeight; y++){
			for(int x = 0; x < mapWidth; x++){
				//each colour is assigned at a certain height value
				for(int i = 0; i < regions.Length; i ++){
					if(noiseMap[x,y] <= regions[i].height){
						colourMap[y * mapWidth + x] =  regions[i].colour; 
						break;
					}
				}
			}
		}
		//find the map display script assigned to the same gameobject 
		MapDisplay display = FindObjectOfType<MapDisplay> ();
		//call the function created in the mapdisplay script
		display.DrawMesh (MeshGenerator.GenerateTerrainMesh (noiseMap, 32, meshHeightCurve), TextureGenerator.TextureFromColourMap (colourMap, mapWidth, mapHeight));
	}

	void Start(){
		//generate a new seed everytime the scene resets so a new world is generated. 
		seed = Random.Range(0,10000);
		GenerateMap();
    }
}

//so the different regions are shown in the inspector and colours can be assigned. 
[System.Serializable]
public struct TerrainType {
	public float height;
	public Color colour; 
}



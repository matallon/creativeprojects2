using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based off procedural generation tutorial series by Sebastian Lague referenced in my Creative Projects 2 document.
//Has obviously been altered to make it slightly more my own but is based upon some of those tutorials. 

public class MapDisplay : MonoBehaviour {
    //to access all the elements needed to create a mesh, and assigns all data generated to them. 
	public Renderer textureRender;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;
    //mesh collider takes data from the filter so that the collisions work properly with procedural generation. 
	public MeshCollider meshCollider;

	public void DrawMesh(MeshData meshData, Texture2D texture){
		meshFilter.mesh = meshData.CreateMesh();
		meshRenderer.material.mainTexture = texture; 
		meshCollider.sharedMesh = meshFilter.mesh;
	}
}
using UnityEngine;
using System.Collections;

//based off procedural generation tutorial series by Sebastian Lague referenced in my Creative Projects 2 document.
//Has obviously been altered to make it slightly more my own but is based upon some of those tutorials. 

public static class MeshGenerator {
	//returns mesh data which is then compiled into a mesh in map generator 
	public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve) {
		//variable set up 
		int width = 200;
		int height = 200;
		float topLeftX = (width - 1) / -2f;
		float topLeftZ = (height - 1) / 2f;
		
		MeshData meshData = new MeshData (width, height);
		int vertexIndex = 0;

		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
                //animation curve adapts the height for underwater/lower areas as just pure noise can look a bit odd 
				meshData.vertices [vertexIndex] = new Vector3 (topLeftX + x, heightCurve.Evaluate(heightMap[x,y]) * heightMultiplier, topLeftZ - y);
				meshData.uvs [vertexIndex] = new Vector2 (x / (float)width, y / (float)height);
				
				//triangulate the mesh 
				if (x < width - 1 && y < height - 1) {
					//need 2 to create squares
					meshData.AddTriangle (vertexIndex, vertexIndex + width + 1, vertexIndex + width);
					meshData.AddTriangle (vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
				}
				vertexIndex++;
			}
		}
		return meshData;
	}
}

//collects all mesh data and combines it 
public class MeshData {
	public Vector3[] vertices;
	public int[] triangles;
	public Vector2[] uvs;

	int triangleIndex;

	public MeshData(int meshWidth, int meshHeight) {
		vertices = new Vector3[meshWidth * meshHeight];
		uvs = new Vector2[meshWidth * meshHeight];
		triangles = new int[(meshWidth-1)*(meshHeight-1)*6];
	}

	//triangulate the mesh so fill the mesh out 
	public void AddTriangle(int a, int b, int c) {
		triangles [triangleIndex] = a;
		triangles [triangleIndex + 1] = b;
		triangles [triangleIndex + 2] = c;
		triangleIndex += 3;
	}

	//collect all the data and create the mesh 
	public Mesh CreateMesh() {
		//assign these values to the Unity mesh creating functions with the same names 
		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;  //needed to add the texture to the mesh, the height map. 
		mesh.RecalculateNormals ();
		return mesh;
	}
}
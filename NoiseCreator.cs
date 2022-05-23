using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based off procedural generation tutorial series by Sebastian Lague referenced in my Creative Projects 2 document.
//Has obviously been altered to make it slightly more my own but is based upon some of those tutorials. 

public static class Noise {
	public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity) {
		//initiase all the variables 
		float maxNoiseHeight = 0;
		float minNoiseHeight = 0;
		float halfWidth = 0;
		float halfHeight = 0;
		float[,] noiseMap = new float[mapWidth,mapHeight];

		//generate random 
		System.Random prng = new System.Random (seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++) {
			octaveOffsets [i] = new Vector2 (prng.Next (-100000, 100000), prng.Next (-100000, 100000));
		}

		if (scale <= 0) {
			scale = 0.0001f;
		}

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
		
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++) {
					float noise1 = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
					float noise2 = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;

					float noiseVal = Mathf.PerlinNoise (noise1, noise2);
					noiseHeight += (noiseVal * 2 - 1) * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight) {
					maxNoiseHeight = noiseHeight;
				} else if (noiseHeight < minNoiseHeight) {
					minNoiseHeight = noiseHeight;
				}
				noiseMap [x, y] = noiseHeight;
			}
		}

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				noiseMap [x, y] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, noiseMap [x, y]);
			}
		}

		return noiseMap;
	}

}
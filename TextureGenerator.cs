using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//based off procedural generation tutorial series by Sebastian Lague referenced in my Creative Projects 2 document.
//Has obviously been altered to make it slightly more my own but is based upon some of those tutorials.  

public static class TextureGenerator {
    //method to create a texture out of a 1d texture map
    public static Texture2D TextureFromColourMap(Color[] colourMap, int width, int height){
        Texture2D texture = new Texture2D(width, height);
        texture.SetPixels (colourMap);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point; //stops it being as blurry 
        texture.Apply();
        return texture; 
    }
}

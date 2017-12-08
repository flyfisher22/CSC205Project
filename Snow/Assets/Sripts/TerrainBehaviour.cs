using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBehaviour : MonoBehaviour {

    // World Variables
    public Terrain ground;      // Terrain being modified
    private TerrainData oldTD;  // Original Terrain Data
    int resX;                   // Size of the terrain horizontally
    int resY;                   // Size of the terrain vertically
    float[,] heights;           // The heights of the terrain

    // Use this for initialization
    void Start () {
        oldTD = ground.terrainData;                                 // Sets the Original Terrain Data
        resX = ground.terrainData.heightmapWidth;                   // Sets the Terrain's Horizontal Size
        resY = ground.terrainData.heightmapHeight;                  // Sets the Terrain's Vertical Size
        heights = ground.terrainData.GetHeights(0, 0, resX, resY);  // Sets the Terrain Heights
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    // Method to create a bump in the terrain with the parameters of the size of bump, and collision position's x and z positions

    public void Grow(float size, float colx, float colz)
    {

        int sx = (int)(colx / ground.terrainData.size.x * resX);        // Translates World Coordinates of collision into Terrain Space Coordinates (Xworld -> Xterrain)
        int sz = (int)(colz / ground.terrainData.size.z * resY);        // Translates World Coordinates of collision into Terrain Space Coordinates (Zworld -> Yterrain)

        // Limits the largest size of growth
        if(size > 3)
        {
            size = 2;
        }

        int length = (int)Mathf.Pow(3, (int)size);                      // Calculates the size of the bump
        float[,] newH = MakeBumpField(size);                            // Makes a bump map corresponding to the size (see MakeBumpField method)

        for (int i = 0; i < length ; i++)                                           // Cycles through the bump map (x)
        {
            for (int j = 0; j < length; j++)                                        // Cycles through the bump map (y)
            {
                newH[i,j] += heights[i+ sx - length / 2, j+ sz - length / 2];       // Adds the current terrain height to the bump map set height from where the collision took place in terrain coordinates
                heights[i + sx - length / 2, j + sz - length / 2] = newH[i, j];     // Sets the bump map new value as the terrain's new value
                print(heights[i + sx - length / 2, j + sz - length / 2] + " ghj");

            }
        }

        //change height
        ground.terrainData.SetHeights(sx - length/2, sz-length/2, newH);

    }

    // method to make a corresponding bump depending on the size inputted
    private float[,] MakeBumpField(float size)
    {
        int length = (int)Mathf.Pow(3, (int)size);          // calculates length of desired bump
        float[,] newH = new float[length, length];          // makes a square map corresponding to the length inputted

        if (size < 1)                                       // if the size is 0-1
        {
            newH = new float[1, 1] {                        // make a 1x1 bump
                {.001f}
            };

        }
        else if (size < 2)                                  // if the size is 1-2
        {                                           
            newH = new float[3, 3] {                        // make a 3x3 bump
                {.002f, .002f, .002f},
                { .002f, .003f, .002f},
                { .002f, .002f, .002f}
            };

        }
        else if (size < 3)                                  // if the size is 2-3
        {
            newH = new float[9, 9] {                        // make a 9x9 bump

                { .002f, .002f, .002f, .002f, .002f, .002f, .002f, .002f, .002f},
                { .002f, .003f, .003f, .003f, .003f, .003f, .003f, .003f, .002f},
                { .002f, .003f, .004f, .004f, .004f, .004f, .004f, .003f, .002f},
                { .002f, .003f, .004f, .005f, .005f, .005f, .004f, .003f, .002f},
                { .002f, .003f, .004f, .005f, .005f, .005f, .004f, .003f, .002f},
                { .002f, .003f, .004f, .005f, .005f, .005f, .004f, .003f, .002f},
                { .002f, .003f, .004f, .004f, .004f, .004f, .004f, .003f, .002f},
                { .002f, .003f, .003f, .003f, .003f, .003f, .003f, .003f, .002f},
                { .002f, .002f, .002f, .002f, .002f, .002f, .002f, .002f, .002f}
            };

        }

        return newH;                                        // return the bump map created
    }


    // Method to reset the height to the original heights at the beginning of the game
    private void ResetH()
    {
        ground.terrainData.SetHeights(0,0,oldTD.GetHeights(0,0,oldTD.heightmapWidth, oldTD.heightmapHeight));  //Sets current heights to oldTerrain's heights
    }
}

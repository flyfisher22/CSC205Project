using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBehaviour : MonoBehaviour {
    public Terrain ground;
    public Terrain old;
    private TerrainData oldTD;
    // Use this for initialization
    void Start () {
        oldTD = old.terrainData;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Grow(float size, float colx, float colz)
    {
        int resX = ground.terrainData.heightmapWidth;
        int resY = ground.terrainData.heightmapHeight;

        int sx = (int)(colx / ground.terrainData.size.x * resX);
        int sz = (int)(colz / ground.terrainData.size.z * resY);

        //Get the heights of the terrain
        float[,] heights = ground.terrainData.GetHeights(0, 0, resX, resY);
        if(size > 3)
        {
            size = 2;
        }
        int length = (int)Mathf.Pow(3, (int)size);
        float[,] newH = new float[length, length];

        if (size < 1)
        {
            newH = new float[1, 1] {
                {.001f}
            };

        }
        else if (size < 2)
        {
            newH = new float[3,3] {
                {.002f, .002f, .002f},
                { .002f, .003f, .002f},
                { .002f, .002f, .002f}
            };

        }
        else if (size < 3)
        {
            newH = new float[9, 9] {
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

        for (int i = 0; i < length ; i++)
        {
            for (int j = 0; j < length; j++)
            {
                newH[i,j] += heights[i+ sx - length / 2, j+ sz - length / 2];
            }
        }

        //change height
        ground.terrainData.SetHeights(sx - length/2, sz-length/2, newH);
        heights = ground.terrainData.GetHeights(0, 0, resX, resY);

    }


    private void ResetH()
    {
        ground.terrainData.SetHeights(0,0,oldTD.GetHeights(0,0,oldTD.heightmapWidth, oldTD.heightmapHeight));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrain : MonoBehaviour
{
    // Start is called before the first frame update

    public float scale = 100f;

    public float mult = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public float speedDiv;
    
    public int depth = 20;
    public int width = 200;
    public int height = 200;

    private BoidController Player;
    void Start()
    {
        offsetX = Random.Range(0f, 999f);
        offsetY = Random.Range(0f, 999f);

        Player = FindObjectOfType<BoidController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(Player.transform.position.x-(width/2),0, Player.transform.position.z-(height / 2));
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width+1;

        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }


    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y)*mult;
                //Debug.Log("Height at (" + x + "," + y + ")  --  " + heights[x, y]);
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = transform.position.z/speedDiv + offsetX + (scale * ((float)x / width));      // what should i multiply or divide the position by so that the terrain changes as it moves seemlessly
        float yCoord = transform.position.x/speedDiv + offsetY + (scale * ((float)y / height));     // what should i multiply or divide the position by so that the terrain changes as it moves seemlessly

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}

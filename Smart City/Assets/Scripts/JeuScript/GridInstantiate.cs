using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridInstantiate : MonoBehaviour
{
    //GameObject = Tile ?
    [SerializeField] GameObject[] tiles;
    [SerializeField] int gridHeight = 10;
    [SerializeField] int gridWidth = 10;
    [SerializeField] float tileSize = 1;

    private Dictionary<Vector2, GameObject> tilesd;

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        tilesd = new Dictionary<Vector2, GameObject>();
        for(int x = 0; x < gridWidth; x++)
        {
            for( int y = 0; y < gridHeight; y++)
            {
                var randomTile = tiles[Random.Range(0, tiles.Length)];
                GameObject tile = Instantiate(randomTile, transform);

                float posX = (x * tileSize - y * tileSize) / 2f;
                float posY = (x * tileSize + y * tileSize) / 4f;
                //System.Math.Abs(posY);

                tile.transform.position = new Vector2(posX, posY);
                tile.name = x + " , " + y;

                tilesd[new Vector2(x, y)] = tile;
            }
        }
    }

    public GameObject getTileAtPosition(Vector2 pos)
    {
        //TODO 2 * pos * tileSize ?
        Vector2 dictionaryKey = new Vector2(2 * pos.y + pos.x, 2 * pos.y - pos.x);
        return tilesd[dictionaryKey];
    }

}

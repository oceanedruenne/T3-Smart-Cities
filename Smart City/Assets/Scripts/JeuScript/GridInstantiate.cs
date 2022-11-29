using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridInstantiate : MonoBehaviour
{
    //GameObject = Tile ?
    [SerializeField] GameObject[] tiles;
    [SerializeField] int gridHeight = 6;
    [SerializeField] int gridWidth = 6;

    private Dictionary<Vector3Int, GameObject> tilesd;

    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        tilesd = new Dictionary<Vector3Int, GameObject>();
        GameObject gridObject = GameObject.Find("Grid");
        Grid grid = gridObject.GetComponent<Grid>();

        int posX = 0;
        int posY = 0;
        int posZ = 0;

        for (posX = 0; posX < 6; posX++)
        {
            for(posY = 0; posY < 6; posY++)
            {
                var randomTile = tiles[Random.Range(0, tiles.Length)];
                GameObject tile = Instantiate(randomTile, transform);

                tile.transform.parent = gridObject.transform;
                tile.transform.position = grid.CellToWorld(new Vector3Int(posX, posY, posZ));
                tile.name = posX + " , " + posY;

                tilesd[new Vector3Int(posX, posY, posZ)] = tile;
            }
        }
    }

    public GameObject getTileAtPosition(Vector3Int pos)
    {
        Vector3Int dictionaryKey = pos;
        return tilesd[dictionaryKey];
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Source.Controller;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject highlight;
    GameManager gm;
    Grid grid;



    private void Start()
    {
        gm = GameObject.Find("Grid").GetComponent<GameManager>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnMouseUp()
    {
        Vector3Int vect = grid.WorldToCell(this.transform.position);
        //Debug.Log(FindObjectOfType<GridInstantiate>().getTileAtPosition(grid.WorldToCell(this.transform.position)));
        GameHandler gh = FindObjectOfType<GameHandler>();
        gh.selectTile(vect.x,vect.y);
    }
}

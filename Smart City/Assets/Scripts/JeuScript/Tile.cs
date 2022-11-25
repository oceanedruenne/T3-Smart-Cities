using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Source.Controller;
using Source.Model;

namespace JeuScript
{
    public class Tile : MonoBehaviour
    {
    [SerializeField] GameObject highlight;
    GameManager gm;
    Grid grid;

    private Sprite sprite;
    private Sprite[] spriteArray;

    private void Start()
    {
        gm = GameObject.Find("Grid").GetComponent<GameManager>();
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        spriteArray = Resources.LoadAll<Sprite>("Textures");
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

    public void changeSprite(BuildType type){
        GameObject me = FindObjectOfType<GridInstantiate>().getTileAtPosition(grid.WorldToCell(this.transform.position));
        SpriteRenderer sr = me.GetComponent<SpriteRenderer>();
        if(type == BuildType.Empty){
            System.Random rnd = new System.Random();
            sr.sprite = spriteArray[rnd.Next(1)*2+1];
        }
        else if(type == BuildType.Office){
            sr.sprite = spriteArray[2];
        }
        else if(type == BuildType.Housing){
            sr.sprite = spriteArray[0];
        }
    }
}
}

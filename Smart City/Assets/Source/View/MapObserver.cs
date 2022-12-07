using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Source.Controller;
using Source.Model;
using Source.View;
using JeuScript;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class MapObserver : ScriptableObject, Observer
{
    GameObject gridObject = GameObject.Find("Grid");
    Grid grid;
    int size;
    GameObject tilePrefab;
    private Dictionary<Vector3Int, GameObject> tilesd;

    //Panel contenant les infos de notre case s�lectionn�e
    private GameObject panelInfos = GameObject.Find("Panel Infos");
    //Infos dans le panel
    private TextMeshProUGUI textLevel = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
    private Image imageObject = GameObject.Find("Sprite").GetComponent<Image>();
    private TextMeshProUGUI textPrice = GameObject.Find("Price").GetComponent<TextMeshProUGUI>();
    private TextMeshProUGUI textIncome = GameObject.Find("Income").GetComponent<TextMeshProUGUI>();

    GameHandler gameHandler;

    public MapObserver(Map map, GameHandler gh){
        genMap(map);
        this.gameHandler = gh;
    }

    public void genMap(Map map){
        this.size = map.buildings.GetLength(0);
        tilePrefab = Resources.Load<GameObject>("Prefabs/Sprite_default");
        grid = gridObject.GetComponent<Grid>();
        generateGrid();
    }

    private void emptyGrid(){

    }

    private void generateGrid()
    {
        tilesd = new Dictionary<Vector3Int, GameObject>();

        int posX = 0;
        int posY = 0;
        int posZ = 0;

        for (posX = 0; posX < size; posX++)
        {
            for(posY = 0; posY < size; posY++)
            {
                GameObject tile = Instantiate(tilePrefab, grid.transform);

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
    
    public void reactTo(Map map){
        int size = map.buildings.GetLength(0);
        Building building;

        for(int x = 0; x<size; x++){
            for(int y = 0; y<size; y++){
                building = map.buildings[x,y];
                getTileAtPosition(new Vector3Int(x,y,0)).GetComponent<Tile>().changeSprite(building.type, building.level);
            }
        }
    }

    public void reactToPos(Map map, uint posx, uint posy){
        Building building = map.buildings[posx,posy];
        getTileAtPosition(new Vector3Int((int)posx,(int)posy,0)).GetComponent<Tile>().changeSprite(building.type, building.level);
    }

    public void UpdateInfoFrom(Map map, uint posx, uint posy){
        Building building = map.buildings[posx,posy];
        Tile tile = getTileAtPosition(new Vector3Int((int)posx,(int)posy,0)).GetComponent<Tile>();
        changeInfos(building.level, map.getBuyCostAt(posx,posy), map.getIncomeAt(posx,posy), tile.getSprite());
    }

    public void hideInfo(){
        panelInfos.SetActive(false);
    }

    private void changeInfos(int level, uint price, uint income, Sprite image)
    {
        //Niveau du b�timent s�lectionner
        textLevel.text = "Niveau " + level.ToString();

        //Apparence
        imageObject.sprite = image;

        //Prix et revenu
        textPrice.text = "Prix : " + price.ToString();
        textIncome.text = "Gain : " + income.ToString();

        panelInfos.SetActive(true);
    }
}

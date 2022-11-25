using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Source.Model;
using Source.View;
using JeuScript;
public class MapObserver : ScriptableObject, Observer
{

    
    public void reactTo(Map map){
        int size = map.buildings.GetLength(0);

        for(int x = 0; x<size; x++){
            for(int y = 0; y<size; y++){
                FindObjectOfType<GridInstantiate>().getTileAtPosition(new Vector3Int(x,y,0)).GetComponent<Tile>().changeSprite(map.buildings[x,y].type);
            }
        }
    }
}

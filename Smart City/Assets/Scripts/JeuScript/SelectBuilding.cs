using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBuilding : MonoBehaviour
{
    GameManager gm;
    private void Start()
    {
        gm = GameObject.Find("Grid").GetComponent<GameManager>();
    }

    /* changeBuilding() : 
       Paramètres : Sprite building
       Permet de changer de building*/
    public void changeBuilding(Sprite building)
    {
        Debug.Log(building);
        Debug.Log("hey");
        gm.building = building;
    }
}

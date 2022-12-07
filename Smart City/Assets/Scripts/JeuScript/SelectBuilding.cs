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
    public void changeBuilding(Sprite building)
    {
        Debug.Log(building);
        Debug.Log("hey");
        gm.building = building;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject highlight;
    GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("Grid").GetComponent<GameManager>();
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        FindObjectOfType<GridInstantiate>().getTileAtPosition(this.transform.position);
        Debug.Log(FindObjectOfType<GridInstantiate>().getTileAtPosition(this.transform.position));
    }

    private void OnMouseUp()
    {
        GameObject tmp = FindObjectOfType<GridInstantiate>().getTileAtPosition(this.transform.position);
        tmp.GetComponent<SpriteRenderer>().sprite = gm.building;
        //TODO corriger le bug higlight qui garde l'ancienne structure.
        tmp.GetComponentInChildren<SpriteRenderer>().sprite = gm.building;
    }
}

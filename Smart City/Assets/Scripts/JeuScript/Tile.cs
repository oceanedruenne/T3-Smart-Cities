using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] GameObject highlight;

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
}

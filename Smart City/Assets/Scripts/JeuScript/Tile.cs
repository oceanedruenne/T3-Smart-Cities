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
        Grid grid;

        //D�claration
        private Sprite sprite;

        private Sprite[] emptySpriteArray;
        private Sprite[] houseSpriteArray;
        private Sprite[] officeSpriteArray;
        private Sprite transportSprite;

        private void Start()
        {
            //R�cup�ration de la grille
            grid = GameObject.Find("Grid").GetComponent<Grid>();

            //Chargement des diff�rentes apparences de nos tiles en fonction du niveau et du type de b�timent
            emptySpriteArray = Resources.LoadAll<Sprite>("Textures/Empty");
            houseSpriteArray = Resources.LoadAll<Sprite>("Textures/House");
            officeSpriteArray = Resources.LoadAll<Sprite>("Textures/Office");
            transportSprite = Resources.Load<Sprite>("Textures/Transport/Sprite_transport.png");

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

        public void changeSprite(BuildType type, int level)
        {
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();

            if(level == 0 || level == 1)
            {
                if (type == BuildType.Empty)
                {
                    System.Random rnd = new System.Random();
                    sr.sprite = emptySpriteArray[rnd.Next(2)];
                }
                else if (type == BuildType.Transport)
                {
                    sr.sprite = transportSprite;
                }
                else if (type == BuildType.Office)
                {
                    sr.sprite = officeSpriteArray[0];
                }
                else if (type == BuildType.Housing)
                {
                    sr.sprite = houseSpriteArray[0];
                }
            }

            if (level == 2 || level == 3)
            {
                if (type == BuildType.Office)
                {
                    sr.sprite = officeSpriteArray[1];
                }
                else if (type == BuildType.Housing)
                {
                    sr.sprite = houseSpriteArray[1];
                }
            }

            if (level == 4 || level == 5)
            {
                if (type == BuildType.Office)
                {
                    sr.sprite = officeSpriteArray[2];
                }
                else if (type == BuildType.Housing)
                {
                    sr.sprite = houseSpriteArray[2];
                }
            }

        }

        public void changeSprite(BuildType type)
        {
            SpriteRenderer sr = this.GetComponent<SpriteRenderer>();

            if (type == BuildType.Empty)
            {
                System.Random rnd = new System.Random();
                sr.sprite = emptySpriteArray[rnd.Next(2)];
            }
            else if (type == BuildType.Transport)
            {
                sr.sprite = transportSprite;
            }
            else if (type == BuildType.Office)
            {
                sr.sprite = officeSpriteArray[0];
            }
            else if (type == BuildType.Housing)
            {
                sr.sprite = houseSpriteArray[0];
            }
        }
    }
}

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

        SpriteRenderer sr;
        bool isSelected = false;

        private Sprite[] emptySpriteArray;
        private Sprite[] houseSpriteArray;
        private Sprite[] officeSpriteArray;
        private Sprite transportSprite;

        private void Start()
        {
            //R�cup�ration de la grille
            grid = GameObject.Find("Grid").GetComponent<Grid>();

            //Récupération du SpriteRenderer
            sr = this.GetComponent<SpriteRenderer>();

            //Chargement des diff�rentes apparences de nos tiles en fonction du niveau et du type de b�timent
            emptySpriteArray = Resources.LoadAll<Sprite>("Textures/Empty");
            houseSpriteArray = Resources.LoadAll<Sprite>("Textures/House");
            officeSpriteArray = Resources.LoadAll<Sprite>("Textures/Office");
            transportSprite = Resources.Load<Sprite>("Textures/Transport/Sprite_transport");

        }

        private void OnMouseEnter()
        {
            if (isSelected)
            {
                sr.color = new Color(0f, 0.5f, 0f, 1f); //Vert foncé
            }
            //else if()
            else
            {
                sr.color = Color.gray;
            }
        }

        private void OnMouseExit()
        {
            if (isSelected)
            {
                sr.color = Color.green;
            }
            else
            {
                sr.color = Color.white; // Pas de couleur
            }
        }

        private void OnMouseUp()
        {
            Vector3Int vect = grid.WorldToCell(this.transform.position);
            Debug.Log(FindObjectOfType<MapObserver>().getTileAtPosition(grid.WorldToCell(this.transform.position)));
            GameHandler gh = FindObjectOfType<GameHandler>();
            //----
            /*gh.currTile Pour récupérer l'ancienne tile, avant d'utiliser selectTile*/
            if (gh.currTile != null)
            {
                gh.currTile.isSelected = false;
                gh.currTile.sr.color = Color.white;
            }
            //----
            gh.selectTile(vect.x,vect.y, this); //On met la tile actuelle (celle qui contient ce code) dans currTile
            isSelected = true;
            sr.color = new Color(0f, 0.5f, 0f, 1f); //Vert foncé
        }

        public void changeSprite(BuildType type, int level)
        {
            if(level == 0 || level == 1)
            {
                if (type == BuildType.Empty)
                {
                    System.Random rnd = new System.Random();
                    this.sr.sprite = emptySpriteArray[rnd.Next(2)];
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

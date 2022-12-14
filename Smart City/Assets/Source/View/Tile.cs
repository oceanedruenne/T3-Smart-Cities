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
        bool isBoosted = false;
        bool isProtected = false;
        bool isTransport = false;

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
                if(isBoosted){
                    sr.color = new Color(1f, 0.2f, 0.2f, 1f); // rouge?
                }
                else if(isProtected){
                    sr.color = Color.magenta;
                }
                else{
                    sr.color = new Color(0f, 0.5f, 0f, 1f); //Vert foncé
                }
            }
            else if (isTransport)
            {
                sr.color = Color.red;
            }
            else
            {
                if(isBoosted){
                    sr.color = new Color(1f, 0.2f, 0.2f, 1f); // rouge?
                }
                else if(isProtected){
                    sr.color = Color.magenta;
                }
                else{
                    sr.color = Color.gray;
                }
            }
        }

        private void OnMouseExit()
        {
            if(isTransport)
            {
                isSelected = false;
                sr.color = Color.white; // Pas de couleur
            }
            else if (isSelected)
            {
                if(isBoosted){
                    sr.color = new Color(1f, 0.5f, 0.4f, 1f); // rouge plus clair?
                }
                else if(isProtected){
                    sr.color = new Color(1f, 0.5f, 1f, 1f); // Magenta? plus clair
                }
                else{
                    sr.color = Color.green;
                }
            }
            else if (isBoosted)
            {
                sr.color = new Color(1f, 0.5f, 0.4f, 1f); // rouge plus clair?
            }
            else if (isProtected)
            {
                sr.color = new Color(1f, 0.5f, 1f, 1f); // Magenta? plus clair
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
            if (!isTransport) //Si ce n'est PAS un transport
            {
                /*gh.currTile Pour récupérer l'ancienne tile, avant d'utiliser selectTile*/
                if (gh.currTile != null)
                {
                    gh.currTile.deselect();
                }
                
                gh.selectTile(vect.x, vect.y, this); //On met la tile actuelle (celle qui contient ce code) dans currTile
                isSelected = true;
                if(isBoosted){
                    sr.color = new Color(1f, 0.2f, 0.2f, 1f); // rouge?
                }
                else if(isProtected){
                    sr.color = Color.magenta;
                }
                else{
                    sr.color = sr.color = new Color(0f, 0.5f, 0f, 1f); //Vert foncé
                }
            }         
        }

        public void Boost(){
            isBoosted = true;
            sr.color = new Color(1f, 0.5f, 0.4f, 1f); // rouge plus clair?
        }

        public void unBoost(){
            isBoosted = false;
            if(isSelected){
                sr.color = Color.green;
            }
            else
            {
                sr.color = Color.white;
            }
        }

        public void Decree(){
            isProtected = true;
            sr.color = new Color(1f, 0.5f, 1f, 1f); // Magenta? plus clair
        }

        public void unDecree(){
            isProtected = false;
            if(isSelected){
                sr.color = Color.green;
            }
            else
            {
                sr.color = Color.white;
            }
        }

        public void deselect(){
            isSelected = false;
            if(isBoosted){
                sr.color = new Color(1f, 0.5f, 0.4f, 1f); // rouge plus clair?
            }
            else if(isProtected){
                sr.color = new Color(1f, 0.5f, 1f, 1f); // Magenta? plsu clair
            }
            else{
                sr.color = Color.white;
            }
        }

        public Sprite getSprite(){
            return this.sr.sprite;
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
                    isTransport = true;
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
                isTransport = true;
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

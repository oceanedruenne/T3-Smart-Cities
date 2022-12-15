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

        //Déclaration
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

        /// <summary>
        /// Démarrage
        /// </summary>
        private void Start()
        {
            //Récupération de la grille
            grid = GameObject.Find("Grid").GetComponent<Grid>();

            //Récupération du SpriteRenderer
            sr = this.GetComponent<SpriteRenderer>();

            //Chargement des diff�rentes apparences de nos tiles en fonction du niveau et du type de b�timent
            emptySpriteArray = Resources.LoadAll<Sprite>("Textures/Empty");
            houseSpriteArray = Resources.LoadAll<Sprite>("Textures/House");
            officeSpriteArray = Resources.LoadAll<Sprite>("Textures/Office");
            transportSprite = Resources.Load<Sprite>("Textures/Transport/Sprite_transport");

        }
        /*
        *OnMouseEnter : fonction 
        Permet de modifier les couleurs de la case lorsqu'elle est selectionnée
        */
        /// <summary>
        /// Lorsque la case est selectionnée
        /// </summary>
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

        /*
        *OnMouseExit : fonction 
        Permet de modifier les couleurs de la case lorsque la souris arrête de survoler la case
        */
        /// <summary>
        /// Lorsque la souris arrête de survoler la case
        /// </summary>
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


        /*
        *OnMouseUp : fonction 
        Permet de modifier les couleurs de la case lorsque la souris passe sur une case
        */
        /// <summary>
        /// Lorsque la souris passe sur une case
        /// </summary>
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

        /*
        *Boost : fonction 
        Permet de modifier les couleurs de la case lorsque elle est boostée
        */
        /// <summary>
        /// Permet d'afficher la case boostée en une autre couleur
        /// </summary>
        public void Boost(){
            isBoosted = true;
            sr.color = new Color(1f, 0.5f, 0.4f, 1f); // rouge plus clair?
        }

        /*
        *unBoost : fonction 
        Permet d'afficher la case d'une couleur différente quand le boost est retiré
        */
        /// <summary>
        /// Permet d'afficher la case d'une couleur différente quand le boost est retiré
        /// </summary>
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


        /*
        *Decree : fonction 
        Permet de changer la couleur de la case quand un décret est mis
        */
        /// <summary>
        /// Permet de changer la couleur de la case quand un décret est mis
        /// </summary>
        public void Decree(){
            isProtected = true;
            sr.color = new Color(1f, 0.5f, 1f, 1f); // Magenta? plus clair
        }

        /*
        *unDecree : fonction 
        Permet de changer la couleur de la case quand un décret est retiré
       */
        /// <summary>
        /// Permet de changer la couleur de la case quand un decree est enlevé
        /// </summary>
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

        /*
        *deSelect : fonction 
        Permet de changer la couleur de la case quand la case n'est plus selectionnée
        */
        /// <summary>
        /// Permet de changer la couleur de la case quand la case est déselectionnée
        /// </summary>
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

        /*
        *getSprite : fonction : Sprite 
        Permet de récupérer le sprite
        */
        /// <summary>
        /// Permet de récupérer le sprite
        /// </summary>
        /// <returns>un Sprite</returns>
        public Sprite getSprite(){
            return this.sr.sprite;
        }


        /*
        *changeSprite : fonction 
        *Paramètres : 
        * level : int : niveau
        * type : BuildType : type de bâtiment
        Permet de changer le sprite
        */
        /// <summary>
        /// Permet de changer le Sprite
        /// </summary>
        /// <param name="type"></param>
        /// <param name="level"></param>
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

        /*
        *changeSprite : fonction 
        *Paramètres : 
        *type : BuildType : type de bâtiment
        Permet de changer le sprite
       */
        /// <summary>
        /// Permet de changer le Sprite
        /// </summary>
        /// <param name="type"></param>
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Source.View;
using TMPro;
using Unity.VisualScripting;

namespace Source.Model
{
    public abstract class Player
    {
        public uint money;
        public uint score;
        public TMP_Text name;
        protected List<PlayerObserver> observers;
        public uint earn;


        /*
        *Player : Constructeur : Player
        *Paramètre : 
        Constructeur de la classe 
        */
        /// <summary>
        /// Constructeur de joueur
        /// </summary>
        public Player()
        {
            this.score = 0;
            this.observers = new List<PlayerObserver>();
            name = new TextMeshProUGUI();
            this.name.text = "test";
            this.earn = 0;
        }


        /*
            *canBuy : fonction : bool
            *Paramètres :
            *map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
            *posx : uint : Position en X sur la carte du bâtiment à acheter
            *posy : uint : Position en Y sur la carte du bâtiment à acheter
            Permet de vérifier si cette case peut être achetée
        */
        /// <summary>
        /// Permet de vérifier si le joueur peut acheter
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>vrai si le joueur peut acheter, sinon faux</returns>
        public abstract bool canBuy(Map map, uint posx, uint posy);


        /*
           *canUpgrade : fonction : bool
           *Paramètres : 
           *map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
           *posx : uint : Position en X sur la carte du bâtiment à acheter
           *posy : uint : Position en Y sur la carte du bâtiment à acheter
           Permet de vérifier si on peut améliorer
           *Variables locales : bool temp
       */
        /// <summary>
        /// Permet de vérifier si le joueur peut améliorer
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>vrai si le joueur peut améliorer, sinon faux</returns>
        public abstract bool canUpgrade(Map map, uint posx, uint posy);




        /*
           *Buy : fonction : bool
           *Paramètres : 
           map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
           posx : uint : Position en X sur la carte du bâtiment à acheter
           posy : uint : Position en Y sur la carte du bâtiment à acheter
           Permet d'acheter, renvoie true si l'opération s'est bien passée, sinon faux
       */
        /// <summary>
        /// Permet de vérifier si l'achat s'est bien déroulé
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>retourne vrai si l'opération s'est bien faite, sinon faux</returns>
        public abstract bool Buy(Map map, uint posx, uint posy);


        /*
           *Upgrade : fonction : bool
           *Paramètres : 
           map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
           posx : uint : Position en X sur la carte du bâtiment à acheter
           posy : uint : Position en Y sur la carte du bâtiment à acheter
           Permet de vérifier si l'amélioration d'un bâtiment s'est bien déroulée
       */
        /// <summary>
        /// Permet de vérifier si l'amélioration d'un bâtiment s'est bien déroulée
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>renvoie vrai si l'amélioration s'est faite, sinon faux</returns>
        public bool Upgrade(Map map, uint posx, uint posy)
        {
            if (canUpgrade(map, posx, posy))
            {
                money -= map.getUpgradeCostAt(posx, posy);
                map.UpgradeAt(posx, posy);
                notifyObservers();
                return true;
            }
            return false;
        }


        /*
            *specialsUse : fonction : 
            *Paramètres :
            * map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
            *posx : uint : Position en X sur la carte du bâtiment à acheter
            *posy : uint : Position en Y sur la carte du bâtiment à acheter
            Permet d'utiliser son pouvoir spécial
        */
        /// <summary>
        /// Permet d'utiliser son pouvoir spécial
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        public abstract void specialsUse(Map map, uint posx, uint posy);


        /*
           *addIncome : fonction 
           *Paramètres : 
           * map : Map : La carte sur laquelle la case va être achetée (La carte de jeu) 
           *Permet d'ajouter les bénéfices
           *Variables locales : money
       */
        /// <summary>
        /// Permet d'ajouter les bénéfices
        /// </summary>
        /// <param name="map"></param>
        public abstract void addIncome(Map map);


        /* 
        * setScore : fonction 
        * Paramètres :
        * map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
        */
        /// <summary>
        /// Permet de fixer le score
        /// </summary>
        /// <param name="map"></param>
        public abstract void setScore(Map map);


        /*
        *addObserver : fonction
        *Paramètres : 
        * observer : MapObserver : Observeur de map
        Permet d'ajouter des observers
        */
        /// <summary>
        /// Permet d'ajouter un observeur
        /// </summary>
        /// <param name="observer"></param>
        public void addObserver(PlayerObserver observer)
        {
            this.observers.Add(observer);
        }


        /*
        *notifyObservers : fonction
        *Paramètres : 
        Permet de notifier les observers
        */
        /// <summary>
        /// Permet de notifier les observers
        /// </summary>
        public void notifyObservers()
        {
            foreach (PlayerObserver observer in observers)
            {
                observer.reactTo(this);
            }
        }

        public void notifyObserversBeginRound(Player player, Map map)
        {
            foreach (PlayerObserver observer in observers)
            {
                observer.reactToBeginRound(this, map);
            }
        }


        /* 
        * isCity : fonction : bool
        * Permet de vérifier si c'est une ville (renvoie toujours true)
        */

        /// <summary>
        /// Permet de vérifier si c'est une ville (renvoie toujours true)
        /// </summary>
        /// <returns>retourne vrai</returns>
        public abstract bool isCity();

        //
        public abstract uint earnAfterTurn(Map map);

        public uint getScore()
        {
            return this.score;
        }

        public uint getMoney()
        {
            return this.money;
        }
    }
}

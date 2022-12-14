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
        protected static uint BASE_MONEY = 10000000;
        public uint money;
        public uint score;
        public TMP_Text name;
        protected List<PlayerObserver> observers;

        /// <summary>
        /// Constructeur de joueur
        /// </summary>
        public Player(){
            this.money = BASE_MONEY;
            this.score = 100;
            this.observers = new List<PlayerObserver>();
            name = new TextMeshProUGUI();
            this.name.text = "test";
        }

        /// <summary>
        /// Permet de vérifier si le joueur peut acheter
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
        public abstract bool canBuy(Map map, uint posx, uint posy);

        /// <summary>
        /// Permet de vérifier si le joueur peut améliorer
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
        public abstract bool canUpgrade(Map map, uint posx, uint posy);

        /// <summary>
        /// Permet de vérifier si l'achat s'est bien déroulé
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
        public abstract bool Buy(Map map, uint posx, uint posy);


        /// <summary>
        /// Permet de vérifier si l'amélioration d'un bâtiment s'est bien déroulée
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
        public bool Upgrade(Map map, uint posx, uint posy){
            if(canUpgrade(map,posx,posy)){
                money -= map.getUpgradeCostAt(posx,posy);
                map.UpgradeAt(posx,posy);
                notifyObservers();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Permet d'utiliser son pouvoir spécial
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        public abstract void specialsUse(Map map, uint posx, uint posy);

        /// <summary>
        /// Permet d'ajouter les bénéfices
        /// </summary>
        /// <param name="map"></param>
        public abstract void addIncome(Map map);

        /// <summary>
        /// Permet de fixer le score
        /// </summary>
        /// <param name="map"></param>
        public abstract void setScore(Map map);

        /// <summary>
        /// Permet d'ajouter un observeur
        /// </summary>
        /// <param name="observer"></param>
        public void addObserver(PlayerObserver observer){
            this.observers.Add(observer);
        }

        /// <summary>
        /// Permet de notifier les observers
        /// </summary>
        public void notifyObservers(){
            foreach(PlayerObserver observer in observers){
                observer.reactTo(this);
            }
        }

        public abstract bool isCity();
    }
}

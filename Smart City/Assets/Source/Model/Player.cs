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
        protected static uint BASE_MONEY = 10000;
        public uint money;
        public uint score;
        public TMP_Text name;
        protected List<PlayerObserver> observers;

        public Player(){
            this.money = BASE_MONEY;
            this.score = 0;
            this.observers = new List<PlayerObserver>();
            name = new TextMeshProUGUI();
            this.name.text = "test";
        }

        public abstract bool canBuy(Map map, uint posx, uint posy);

        public abstract bool canUpgrade(Map map, uint posx, uint posy);

        public abstract bool Buy(Map map, uint posx, uint posy);

        public bool Upgrade(Map map, uint posx, uint posy){
            if(canUpgrade(map,posx,posy)){
                money -= map.getUpgradeCostAt(posx,posy);
                map.UpgradeAt(posx,posy);
                notifyObservers();
                return true;
            }
            return false;
        }

        public abstract void specialsUse(Map map, uint posx, uint posy);

        public abstract void addIncome(Map map);

        public abstract void setScore(Map map);

        public void addObserver(PlayerObserver observer){
            this.observers.Add(observer);
        }

        public void notifyObservers(){
            foreach(PlayerObserver observer in observers){
                observer.reactTo(this);
            }
        }

        public abstract bool isCity();
        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Source.View;

namespace Source.Model
{
    public abstract class Player
    {
        protected static uint BASE_MONEY = 10000000;
        protected uint money;
        protected uint score;
        protected List<PlayerObserver> observers;

        public Player(){
            this.money = BASE_MONEY;
            this.score = 0;
            this.observers = new List<PlayerObserver>();
        }

        public bool canBuy(Map map, uint posx, uint posy){
            return map.getBuyCostAt(posx,posy) <= money;
        }

        public bool canUpgrade(Map map, uint posx, uint posy){
            return map.getUpgradeCostAt(posx,posy) <= money;
        }

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

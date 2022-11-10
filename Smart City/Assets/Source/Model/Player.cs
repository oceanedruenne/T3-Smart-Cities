using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Source;

namespace Source.Model
{
    public abstract class Player : MonoBehaviour
    {
        private static int BASE_MONEY = 1000;
        private int money;
        private uint score;
        private List<Observer> observers;

        public Player(){
            this.money = BASE_MONEY;
            this.score = 0;
            this.observers = new List<Observer>();
        }

        public bool canBuy(Map map, uint posx, uint posy){
            return map.getBuyCostAt(posx,posy) <= money;
        }

        public bool canUpgrade(Map map, uint posx, uint posy){
            return map.getUpgradeCostAt(posx,posy) <= money;
        }

        public void Buy(Map map, uint posx, uint posy);

        public void Upgrade(Map map, uint posx, uint posy){
            money -= map.getAt(posx,posy).getUpgradeCost;
            map.UpgradeAt(posx,posy);
        }

        public void specialsUse(Map map, uint posx, uint posy);

        public void addIncome(Map map);

        public void addObserver(Observer observer){
            this.observers.Add(observer);
        }
        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
    }
}

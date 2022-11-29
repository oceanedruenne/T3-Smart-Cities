using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Model
{
    public class Company : Player
    {
        public override bool Buy(Map map, uint posx, uint posy){
            if(canBuy(map, posx, posy)){
                money -= map.getBuyCostAt(posx,posy);
                map.buildAt(BuildType.Office, posx, posy);
                notifyObservers();
                return true;
            }
            return false;
        }

        public override bool canBuy(Map map, uint posx, uint posy){
            bool temp = map.getBuyCostAt(posx,posy) <= money;
            switch (map.getTypeAt(posx,posy)){
                case BuildType.Housing:
                case BuildType.Empty:
                    temp &= true;
                    break;
                default:
                    temp &= false;
                    break;
            }
            temp &= !map.getDecree(posx,posy);
            return temp;
        }

        public override bool canUpgrade(Map map, uint posx, uint posy){
            bool temp = map.getBuyCostAt(posx,posy) <= money;
            switch (map.getTypeAt(posx,posy)){
                case BuildType.Office:
                    temp &= true;
                    break;
                default:
                    temp &= false;
                    break;
            }
            temp &= !map.getDecree(posx,posy);
            return temp;
        }

        public override void specialsUse(Map map, uint posx, uint posy){
            map.setBoost(posx,posy);
        }

        public override void addIncome(Map map){
            money += map.getIncomeFromType(BuildType.Office);
            notifyObservers();
        }

        public override bool isCity(){
            return false;
        }
        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
    }
}

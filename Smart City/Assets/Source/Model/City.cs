using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Model
{
    public class City : Player
    {
        public override bool Buy(Map map, uint posx, uint posy){
            if(canBuy(map,posx,posy)){
                money -= map.getBuyCostAt(posx,posy);
                map.buildAt(BuildType.Housing, posx, posy);
                notifyObservers();
                return true;
            }
            return false;
        }

        public override void specialsUse(Map map, uint posx, uint posy){
            map.setDecree(posx, posy);
        }

        public override void addIncome(Map map){
            money += map.getIncomeFromType(BuildType.Housing);
            notifyObservers();
        }

        public override bool isCity(){
            return true;
        }
        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
    }
}

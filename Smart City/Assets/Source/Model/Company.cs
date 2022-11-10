using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Model
{
    public class Company : Player
    {
        public void Buy(Map map, uint posx, uint posy){
            money -= map.getBuyCostAt(posx,posy);
            map.buildAt(BuildType.Office, posx, posy);
        }

        public void specialsUse(Map map, uint posx, uint posy){
            map.setBoost(posx,posy);
        }

        public void addIncome(Map map){
            money += map.getIncomeFromType(BuildType.Office);
        }
        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
    }
}

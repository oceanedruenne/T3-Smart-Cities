using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Model
{
    public class City : Player
    {
        public void Buy(Map map, uint posx, uint posy){
            money -= map.getBuyCostAt(posx,posy);
            map.buildAt(BuildType.Housing, posx, posy);
        }

        public void specialsUse(Map map, uint posx, uint posy){
            map.setDecree(posx, posy);
        }

        public void addIncome(Map map){
            money += map.getIncomeFromType(BuildType.Housing);
        }
        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
    }
}

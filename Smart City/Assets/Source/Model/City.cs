using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Model
{
    public class City : Player
    {
        int TURN_BONUS = 0;   
        /*
            *schéma : Buy : bool
            *Paramètre : 
            map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
            posx : uint : Position en X sur la carte du bâtiment à acheter
            posy : uint : Position en Y sur la carte du bâtiment à acheter
            *Keskecé :
            *Varibales locales :
        */
        public override bool Buy(Map map, uint posx, uint posy){
            if(canBuy(map,posx,posy)){
                money -= map.getBuyCostAt(posx,posy);
                map.buildAt(BuildType.Housing, posx, posy);
                notifyObservers();
                return true;
            }
            return false;
        }

        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
        public override bool canBuy(Map map, uint posx, uint posy){
            bool temp = (map.getBuyCostAt(posx,posy) <= money);
            switch (map.getTypeAt(posx,posy)){
                case BuildType.Office:
                case BuildType.Empty:
                    temp &= true;
                    break;
                default:
                    temp &= false;
                    break;
            }
            return temp;
        }
        
        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
        public override bool canUpgrade(Map map, uint posx, uint posy){
            bool temp = map.getBuyCostAt(posx,posy) <= money;
            temp &= map.getUnderMaxLevel(posx,posy);
            switch (map.getTypeAt(posx,posy)){
                case BuildType.Housing:
                    temp &= true;
                    break;
                default:
                    temp &= false;
                    break;
            }
            return temp;
        }

        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
        public override void specialsUse(Map map, uint posx, uint posy){
            map.setDecree(posx, posy);
        }

        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
        public override void addIncome(Map map){
            money += (uint)TURN_BONUS + map.getIncomeFromType(BuildType.Housing);
            notifyObservers();
        }

        public override void setScore(Map map)
        {
            score = map.getScoreFromType(BuildType.Housing);
        }

        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
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

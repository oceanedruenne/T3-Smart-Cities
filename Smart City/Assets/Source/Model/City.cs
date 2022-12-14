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
            *Paramètres : 
            map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
            posx : uint : Position en X sur la carte du bâtiment à acheter
            posy : uint : Position en Y sur la carte du bâtiment à acheter
            *Keskecé : Permet d'acheter
        */

        /// <summary>
        /// Permet d'acheter un bâtiment aux coordonnées X Y
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
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
            *schéma : canBuy : bool
            *Paramètres :
            * map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
            *posx : uint : Position en X sur la carte du bâtiment à acheter
            *posy : uint : Position en Y sur la carte du bâtiment à acheter
            *Keskecé : Permet de vérifier si cette case peut être achetée
        */

        /// <summary>
        /// Permet de vérifier si on peut acheter
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
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
            *schéma : canUpgrade : bool
            *Paramètres : 
            * map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
            *posx : uint : Position en X sur la carte du bâtiment à acheter
            *posy : uint : Position en Y sur la carte du bâtiment à acheter
            *Keskecé : Permet de vérifier si on peut améliorer
            *Variables locales : bool temp
        */

        /// <summary>
        /// Permet de vérifier si on peut modifier une case
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
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
            *schéma : specialsUse
            *Paramètres :
            * map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
            *posx : uint : Position en X sur la carte du bâtiment à acheter
            $posy : uint : Position en Y sur la carte du bâtiment à acheter
            *Keskecé : permet d'utiliser son pouvoir spécial
        */

        /// <summary>
        /// Permet d'utiliser son pouvoir spécial
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        public override void specialsUse(Map map, uint posx, uint posy){
            map.setDecree(posx, posy);
        }

        /*
            *schéma : addIncome 
            *Paramètres : map : Map : La carte sur laquelle la case va être achetée (La carte de jeu) 
            *Keskecé : permet d'ajouter les bénéfices
            *Varibales locales : money
        */
        /// <summary>
        /// Permet d'ajouter les bénéfices
        /// </summary>
        /// <param name="map"></param>
        public override void addIncome(Map map){
            money += (uint)TURN_BONUS + map.getIncomeFromType(BuildType.Housing);
            notifyObservers();
        }

        /// <summary>
        /// Permet d'attribuer une valeur au score
        /// </summary>
        /// <param name="map"></param>
        public override void setScore(Map map)
        {
            score = map.getScoreFromType(BuildType.Housing);
        }

        
        public override bool isCity(){
            return true;
        }
    }
}

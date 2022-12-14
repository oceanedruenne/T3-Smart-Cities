using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Model
{
    public class Company : Player
    {
        int TURN_BONUS = 0;

        /*
           *Buy : fonction : bool
           *Paramètres : 
           map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
           posx : uint : Position en X sur la carte du bâtiment à acheter
           posy : uint : Position en Y sur la carte du bâtiment à acheter
           Permet d'acheter, renvoie true si l'opération s'est bien passée, sinon faux
       */
        /// <summary>
        /// Permet d'acheter
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>Vrai si cela a été fait, sinon faux</returns>
        public override bool Buy(Map map, uint posx, uint posy){
            if(canBuy(map, posx, posy)){
                money -= map.getBuyCostAt(posx,posy);
                map.buildAt(BuildType.Office, posx, posy);
                notifyObservers();
                return true;
            }
            return false;
        }


        /*
         *canBuy : fonction : bool
         *Paramètres :
         *map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
         *posx : uint : Position en X sur la carte du bâtiment à acheter
         *posy : uint : Position en Y sur la carte du bâtiment à acheter
         Permet de vérifier si cette case peut être achetée
        */

        /// <summary>
        /// Permet de vérifier si l'on peut acheter 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>renvoie vrai si le bâtiment peut être acheté, sinon faux</returns>
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

        /*
           *canUpgrade : fonction : bool
           *Paramètres : 
           *map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
           *posx : uint : Position en X sur la carte du bâtiment à acheter
           *posy : uint : Position en Y sur la carte du bâtiment à acheter
           Permet de vérifier si on peut améliorer
           *Variables locales : bool temp
       */
        /// <summary>
        /// Permet de vérifier si l'on peut améliorer
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>renvoie vrai si le bâtiment peut être amélioré, sinon faux</returns>
        public override bool canUpgrade(Map map, uint posx, uint posy){
            bool temp = map.getBuyCostAt(posx,posy) <= money;
            temp &= map.getUnderMaxLevel(posx,posy);
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


        /*
            *specialsUse : fonction : 
            *Paramètres :
            * map : Map : La carte sur laquelle la case va être achetée (La carte de jeu)
            *posx : uint : Position en X sur la carte du bâtiment à acheter
            *posy : uint : Position en Y sur la carte du bâtiment à acheter
            Permet d'utiliser son pouvoir spécial
        */

        /// <summary>
        /// Permet d'utiliser son pouvoir spécial
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        public override void specialsUse(Map map, uint posx, uint posy){
            map.setBoost(posx,posy);
        }

        /*
           *addIncome : fonction 
           *Paramètres : 
           * map : Map : La carte sur laquelle la case va être achetée (La carte de jeu) 
           *Permet d'ajouter les bénéfices
           *Variables locales : money
       */
        /// <summary>
        /// Permet d'ajouter les bénéfices
        /// </summary>
        /// <param name="map"></param>
        public override void addIncome(Map map){
            money += (uint)TURN_BONUS + map.getIncomeFromType(BuildType.Office);
            notifyObservers();
        }

        /*
           *getIncome : fonction : uint
           *Paramètres : 
           * map : Map : La carte sur laquelle la case va être achetée (La carte de jeu) 
           *Permet de récupérer les bénéfices du bâtiment
           *Variables locales : tempMoney
       */
        /// <summary>
        /// Permet de récupérer les bénéfices
        /// </summary>
        /// <param name="map"></param>
        /// <returns>Les bénéfices du bâtiment</returns>
        private uint getIncome(Map map){
            uint tempMoney = 0;
            tempMoney += map.getIncomeFromType(BuildType.Office);
            return tempMoney;
        }

        /*
           *setScore : fonction 
           *Paramètres : 
           * map : Map : La carte sur laquelle la case va être achetée (La carte de jeu) 
           * Permet de fixer le score
       */

        /// <summary>
        /// Permet d'établir le score
        /// </summary>
        /// <param name="map"></param>
        public override void setScore(Map map)
        {
            score = getIncome(map);
        }


        /* 
        * isCity : fonction : bool
        * Permet de vérifier si c'est une ville (renvoie toujours true)
        */

        /// <summary>
        /// Permet de vérifier si c'est une ville (renvoie toujours true)
        /// </summary>
        /// <returns>retourne vrai</returns>
        public override bool isCity(){
            return false;
        }
    }
}

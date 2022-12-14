using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Model
{
    public class Company : Player
    {
        int TURN_BONUS = 0;
        /// <summary>
        /// Permet d'acheter
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
        public override bool Buy(Map map, uint posx, uint posy){
            if(canBuy(map, posx, posy)){
                money -= map.getBuyCostAt(posx,posy);
                map.buildAt(BuildType.Office, posx, posy);
                notifyObservers();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Permet de vérifier si l'on peut acheter 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Permet de vérifier si l'on peut améliorer
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Permet d'utiliser son pouvoir spécial
        /// </summary>
        /// <param name="map"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        public override void specialsUse(Map map, uint posx, uint posy){
            map.setBoost(posx,posy);
        }

        /// <summary>
        /// Permet d'ajouter les bénéfices
        /// </summary>
        /// <param name="map"></param>
        public override void addIncome(Map map){
            money += (uint)TURN_BONUS + map.getIncomeFromType(BuildType.Office);
            notifyObservers();
        }

        /// <summary>
        /// Permet de récupérer les bénéfices
        /// </summary>
        /// <param name="map"></param>
        /// <returns>uint</returns>
        private uint getIncome(Map map){
            uint tempMoney = 0;
            tempMoney += map.getIncomeFromType(BuildType.Office);
            return tempMoney;
        }

        /// <summary>
        /// Permet d'établir le score
        /// </summary>
        /// <param name="map"></param>
        public override void setScore(Map map)
        {
            score = getIncome(map);
        }

        public override bool isCity(){
            return false;
        }
    }
}

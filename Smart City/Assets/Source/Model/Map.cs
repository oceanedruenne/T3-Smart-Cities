using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Source.Model
{
    public class Map : MonoBehaviour
    {
        private Building[,] buildings;
        private uint[] decree = new uint[2];
        private uint[] boost = new uint[2];
        private List<Observer> observers;
        /*
            *Map : Constructeur : Map
            *Paramètre :
            *Keskecé : 
            *Varibales locales :
        */
        public Map(uint size = 6){
            this.buildings = new Building[size, size];
            fillMapAleaNoSeedEqualChance();
            observers = new List<Observer>();
        }

        fillMapEmpty(){
            uint size =  buildings.GetLength(0);
            for(int i = 0;i < size;i++){
                for(int j = 0;j < size;j++){
                    buildings[i,j] = new Building(BuildType.Empty);
                }
            }
        }

        public void fillMapAleaNoSeedEqualChance(){
            uint size =  buildings.GetLength(0);
            Random rand = new Random();
            for(int i = 0;i < size;i++){
                for(int j = 0;j < size;j++){
                    uint rnd = rand.Next(Enum.GetNames(BuildType).Length);
                    buildings[i,j] = new Building((BuildType)rnd);
                }
            }
        }

        public uint getUpgradeCostAt(uint posx, uint posy){
            return buildings[posx, posy].getUpgradeCost;
        }

        public uint getBuyCostAt(uint posx, uint posy){
            return buildings[posx,posy].getBuyCost;
        }

        public void buildAt(BuildType type, uint posx, uint posy){
            if(posx > buildings.GetLength(0) || posy > buildings.GetLength(0))
            {
                throw new System.Exception("Map : Build : posx ou posy en dehors du tableau");
            }
            Building ancien = buildings[posx,posy];
            buildings[posx,posy] = new Building(type, ancien.buyMalus, ancien.level);
        }

        public void destroyAt(uint posx, uint posy){
            buildings[posx,posy] = new Building(BuildType.Empty);
        }

        public void UpgradeAt(uint posx, uint posy){
            buildings[posx,posy].upgrade();
        }

        public void setDecree(uint posx, uint posy){
            decree[0] = posx;
            decree[1] = posy;
        }

        public bool getDecree(uint posx, uint posy){
            return decree[0] = posx && decree[1] = posy;
        }

        public void setBoost(uint posx, uint posy){
            boost[0] = posx;
            boost[1] = posy;
        }

        public uint getIncomeFromType(BuildType type){
            uint total = 0;
            foreach(Building building in buildings){
                if(building.type == type){
                    total += building.getIncome();
                }
            }
            Building boosted = buildings[boost[0],boost[1]];
            if(type == boosted.type){
                total += boosted.getIncome();
            }
            return total;
        }

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

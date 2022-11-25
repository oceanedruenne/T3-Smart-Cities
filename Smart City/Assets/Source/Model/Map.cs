using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Source.View;

namespace Source.Model
{
    public class Map
    {
        public Building[,] buildings;
        private uint[] decree = new uint[2];
        private uint[] boost = new uint[2];
        private List<MapObserver> observers;
        /*
            *Map : Constructeur : Map
            *Paramètre :
            *Keskecé : 
            *Varibales locales :
        */
        public Map(uint size = 6){
            this.buildings = new Building[size, size];
            fillMapEmpty();
            buildings[3, 5] = Building.createTransport();
            observers = new List<MapObserver>();
            notifyObservers();
        }

        public void fillMapEmpty(){
            int size =  buildings.GetLength(0);
            for(int i = 0;i < size;i++){
                for(int j = 0;j < size;j++){
                    buildings[i,j] = new Building(BuildType.Empty);
                }
            }
        }

        public void fillMapRandNoSeedEqualChance(){
            int size =  buildings.GetLength(0);
            System.Random rand = new System.Random();
            for(int i = 0;i < size;i++){
                for(int j = 0;j < size;j++){
                    int rnd = rand.Next(Enum.GetNames(typeof(BuildType)).Length);
                    buildings[i,j] = new Building((BuildType)rnd);
                }
            }
        }

        public uint getUpgradeCostAt(uint posx, uint posy){
            return buildings[posx, posy].getUpgradeCost();
        }

        public uint getBuyCostAt(uint posx, uint posy){
            return buildings[posx,posy].getBuyCost();
        }

        public void buildAt(BuildType type, uint posx, uint posy){
            if(posx > buildings.GetLength(0) || posy > buildings.GetLength(0))
            {
                throw new System.Exception("Map : Build : posx ou posy en dehors du tableau");
            }
            Building ancien = buildings[posx,posy];
            buildings[posx,posy] = new Building(type, ancien.buyMalus, ancien.level);
            notifyObservers();
        }

        public void destroyAt(uint posx, uint posy){
            buildings[posx,posy] = new Building(BuildType.Empty);
            notifyObservers();
        }

        public void UpgradeAt(uint posx, uint posy){
            Building ancien = buildings[posx,posy];
            buildings[posx,posy] = new Building(ancien.type, ancien.buyMalus, ancien.level + 1);
        }

        public void setDecree(uint posx, uint posy){
            decree[0] = posx;
            decree[1] = posy;
        }

        public bool getDecree(uint posx, uint posy){
            return (decree[0] == posx) && (decree[1] == posy);
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

        public void addObserver(MapObserver observer){
            this.observers.Add(observer);
        }

        public void notifyObservers(){
            foreach(MapObserver observer in observers){
                observer.reactTo(this);
            }
        }

        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
    }
}

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

        public BuildType getTypeAt(uint posx, uint posy){
            return buildings[posx,posy].type;
        }

        public bool getUnderMaxLevel(uint posx, uint posy){
            return buildings[posx,posy].level < 6;
        }

        public void buildAt(BuildType type, uint posx, uint posy){
            if(posx > buildings.GetLength(0) || posy > buildings.GetLength(0))
            {
                throw new System.Exception("Map : Build : posx ou posy en dehors du tableau");
            }
            Building old = buildings[posx,posy];
            buildings[posx,posy] = new Building(type, old.buyMalus+Building.MALUS_INCREASE, old.level);
            notifyObserversPos(posx, posy);
        }

        public void buildAtNoMalus(BuildType type, uint posx, uint posy){
            if(posx > buildings.GetLength(0) || posy > buildings.GetLength(0))
            {
                throw new System.Exception("Map : Build : posx ou posy en dehors du tableau");
            }
            Building old = buildings[posx,posy];
            buildings[posx,posy] = new Building(type, old.buyMalus, old.level);
            notifyObserversPos(posx, posy);
        }

        public void destroyAt(uint posx, uint posy){
            if(posx > buildings.GetLength(0) || posy > buildings.GetLength(0))
            {
                throw new System.Exception("Map : Build : posx ou posy en dehors du tableau");
            }
            buildings[posx,posy] = new Building(BuildType.Empty);
            notifyObserversPos(posx, posy);
        }

        public void UpgradeAt(uint posx, uint posy){
            if(posx > buildings.GetLength(0) || posy > buildings.GetLength(0))
            {
                throw new System.Exception("Map : Build : posx ou posy en dehors du tableau");
            }
            Building old = buildings[posx,posy];
            buildings[posx,posy] = new Building(old.type, old.buyMalus, old.level + 1);
            notifyObserversPos(posx, posy);
        }


        //GESTION CAPACITE SPECIALE
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

        public bool getBoost(uint posx, uint posy){
            return (boost[0] == posx) && (boost[1] == posy);
        }

        //GESTION SCORE ET REVENU
        public uint getIncomeAt(uint posx, uint posy){
            uint income = 0;
            BuildType type = getTypeAt(posx,posy);
            if(type == BuildType.Housing || type == BuildType.Office){
                income += buildings[posx,posy].getIncome();
                if(isAdjacentToTransport(posx,posy)){
                    income += income/2;
                }
                if(getBoost(posx,posy)){
                    income += income/2;
                }
            }
            return income;
        }

        public uint getIncomeAfterUpgradeAt(uint posx, uint posy){
            uint income = 0;
            BuildType type = getTypeAt(posx,posy);
            if(type == BuildType.Housing || type == BuildType.Office){
                income += buildings[posx,posy].getIncomeAfterUpgrade();
                if(isAdjacentToTransport(posx,posy)){
                    income += income/2;
                }
                if(getBoost(posx,posy)){
                    income += income/2;
                }
            }
            income -= this.getIncomeAt(posx, posy);
            return income;
        }

        public bool isAdjacentToTransport(uint x, uint y){
            bool res = false;
            int size = buildings.GetLength(0);
            if(y+1 < size && buildings[x,y+1].type == BuildType.Transport){
                res = true;
            }
            if(!res && y > 0 && buildings[x,y-1].type == BuildType.Transport){
                res = true;
            }
            if(!res && x+1 < size && buildings[x+1,y].type == BuildType.Transport){
                res = true;
            }
            if(!res && x > 0 && buildings[x-1,y].type == BuildType.Transport){
                res = true;
            }
            return res;
        }

        public uint getIncomeFromType(BuildType type){
            uint total = 0;
            int size = buildings.GetLength(0);
            for(uint x=0;x<size; x++){
                for(uint y=0;y<size; y++){
                    Building building = buildings[x,y];
                    if(building.type == type){
                        total += getIncomeAt(x,y);
                    }
                }
            }
            return total;
        }

        public uint getScoreFromType(BuildType type){
            uint total= 0;
            int size = buildings.GetLength(0);
            for(uint x=0;x<size; x++){
                for(uint y=0;y<size; y++){
                    Building building = buildings[x,y];
                    if(building.type == type){
                        if(isAdjacentToTransport(x,y)){
                            total+=(uint)(400*(building.level+1)*building.buyMalus);
                        }
                        else{
                            total+=(uint)(100*(building.level+1)*building.buyMalus);
                        }
                    }
                }
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

        public void notifyObserversPos(uint x, uint y){
            foreach(MapObserver observer in observers){
                observer.reactToPos(this, x, y);
            }
        }

        public void notifyObserverChange(uint x, uint y){
            foreach(MapObserver observer in observers){
                observer.UpdateInfoFrom(this, x, y);
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

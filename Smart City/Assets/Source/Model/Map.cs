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

        /// <summary>
        /// Constructeur de Map
        /// </summary>
        /// <param name="size"></param>
        public Map(uint size = 6){
            this.buildings = new Building[size, size];
            fillMapEmpty();
            observers = new List<MapObserver>();
        }

        /// <summary>
        /// Permet de remplir la Map
        /// </summary>
        public void fillMapEmpty(){
            int size =  buildings.GetLength(0);
            for(int i = 0;i < size;i++){
                for(int j = 0;j < size;j++){
                    buildings[i,j] = new Building(BuildType.Empty);
                }
            }
            buildings[3, 5] = Building.createTransport();
        }
/*
        public void fillMapRandom1(){
            int size = buildings.GetLength(0);
            System.Random rand = new System.Random();
            for(int i = 0;i < size;i++){
                for(int j = 0;j < size;j++){
                    buildings[i,j] = new Building(BuildType.Empty);
                }
            }

            int[][] coordinates = new int[][size/2];
            for(int i=0;i<size/2;i++){
                int x = rand.Next(size);
                int y = rand.Next(size);
                if(getDist(1,new int[]{x,y},coordinates)){
                    buildings[x,y] = Building.createTransport();
                }
            }
        }

        private bool getDist(int minDist, int[] coordinate, int[][] coordinates){
            bool temp = true;
            float vectStart = Math.Sqrt(Math.Pow(coordinate[0],2)+Math.Pow(coordinate[1],2));
            foreach(int[] coord in coordinates){
                float vectEnd = Math.Sqrt(Math.Pow(coord[0],2)+Math.Pow(coord[1],2));
                if(vectEnd - vectStart < minDist){
                    temp = false;
                    break;
                }
            }
            return temp;
        }
*/
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

        /// <summary>
        /// Permet de récupérer le coût d'amélioration à la case ayant comme coordonnées les entiers passés en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>uint</returns>
        public uint getUpgradeCostAt(uint posx, uint posy){
            return buildings[posx, posy].getUpgradeCost();
        }

        /// <summary>
        /// Permet de récupérer le coût d'achat de la case ayant comme coordonnées les paramètres de la fonction
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns></returns>
        public uint getBuyCostAt(uint posx, uint posy){
            return buildings[posx,posy].getBuyCost();
        }

        /// <summary>
        /// Permet de récupérer le type de bâtiment de la case ayant comme coordonées les paramètres de la fonction
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>BuildType</returns>
        public BuildType getTypeAt(uint posx, uint posy){
            return buildings[posx,posy].type;
        }

        /// <summary>
        /// Permet de vérifier si le niveau d'amélioration est inférieur à 6
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
        public bool getUnderMaxLevel(uint posx, uint posy){
            return buildings[posx,posy].level < 6;
        }

        /// <summary>
        /// Permet de construire sur la case ayant comme coordonnées les paramètres de la fonction
        /// </summary>
        /// <param name="type"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <exception cref="System.Exception"></exception>
        public void buildAt(BuildType type, uint posx, uint posy){
            if(posx > buildings.GetLength(0) || posy > buildings.GetLength(0))
            {
                throw new System.Exception("Map : Build : posx ou posy en dehors du tableau");
            }
            Building old = buildings[posx,posy];
            buildings[posx,posy] = new Building(type, old.buyMalus+Building.MALUS_INCREASE, old.level);
            notifyObserversPos(posx, posy);
        }

        /// <summary>
        /// Permet de construire sans malus dans la case ayant comme coordonnées les paramètres de la fonction
        /// </summary>
        /// <param name="type"></param>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <exception cref="System.Exception"></exception>
        public void buildAtNoMalus(BuildType type, uint posx, uint posy){
            if(posx > buildings.GetLength(0) || posy > buildings.GetLength(0))
            {
                throw new System.Exception("Map : Build : posx ou posy en dehors du tableau");
            }
            Building old = buildings[posx,posy];
            buildings[posx,posy] = new Building(type, old.buyMalus, old.level);
            notifyObserversPos(posx, posy);
        }

        /// <summary>
        /// Permet de détruire un bâtiment à la case dont les coordonnées sont passées en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <exception cref="System.Exception"></exception>
        public void destroyAt(uint posx, uint posy){
            if(posx > buildings.GetLength(0) || posy > buildings.GetLength(0))
            {
                throw new System.Exception("Map : Build : posx ou posy en dehors du tableau");
            }
            buildings[posx,posy] = new Building(BuildType.Empty);
            notifyObserversPos(posx, posy);
        }

        /// <summary>
        /// Permet d'améliorer à la case dont les coordonnées sont passées en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <exception cref="System.Exception"></exception>
        public void UpgradeAt(uint posx, uint posy){
            if(posx > buildings.GetLength(0) || posy > buildings.GetLength(0))
            {
                throw new System.Exception("Map : Build : posx ou posy en dehors du tableau");
            }
            Building old = buildings[posx,posy];
            buildings[posx,posy] = new Building(old.type, old.buyMalus, old.level + 1);
            notifyObserversPos(posx, posy);
        }


        /// <summary>
        /// Permet de gérer le decree de la case dont les coordonnées sont passées en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        public void setDecree(uint posx, uint posy){
            decree[0] = posx;
            decree[1] = posy;
        }

        /// <summary>
        /// Permet de vérifier s'il y a un decree
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
        public bool getDecree(uint posx, uint posy){
            return (decree[0] == posx) && (decree[1] == posy);
        }

        /// <summary>
        /// Permet de mettre en place le boost
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        public void setBoost(uint posx, uint posy){
            boost[0] = posx;
            boost[1] = posy;
        }

        /// <summary>
        /// Permet de vérifier si un boost est placé sur la case dont les coordonnées sont passées en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>bool</returns>
        public bool getBoost(uint posx, uint posy){
            return (boost[0] == posx) && (boost[1] == posy);
        }

        /// <summary>
        /// Permet de récupérer les bénéfices du bâtiment dont les coordonnées de sa case sont passées en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>uint</returns>
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

        /// <summary>
        /// Permet de récupérer les bénéfices du bâtiment après son amélioration dont les coordonnées de sa case sont passées en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>uint</returns>
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
            return income;
        }

        /// <summary>
        /// Permet de vérifier si la case passée en paramètre est proche d'une case contenant un transport
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>bool</returns>
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

        /// <summary>
        /// Permet de récupérer les revenus de tous les bâtiments d'un type donné
        /// </summary>
        /// <param name="type"></param>
        /// <returns>uint</returns>
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

        /// <summary>
        /// Permet de récupérer le score de tous les bâtiments d'un type donné
        /// </summary>
        /// <param name="type"></param>
        /// <returns>uint</returns>
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

        /// <summary>
        /// Permet d'ajouter des observers
        /// </summary>
        /// <param name="observer"></param>
        public void addObserver(MapObserver observer){
            this.observers.Add(observer);
        }

        /// <summary>
        /// Permet de notifier les observers
        /// </summary>
        public void notifyObservers(){
            foreach(MapObserver observer in observers){
                observer.reactTo(this);
            }
        }

        /// <summary>
        /// Permet de notifier les observers des changements de position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void notifyObserversPos(uint x, uint y){
            foreach(MapObserver observer in observers){
                observer.reactToPos(this, x, y);
            }
        }

        /// <summary>
        /// Permet de notifier les observers
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void notifyObserverChange(uint x, uint y){
            foreach(MapObserver observer in observers){
                observer.UpdateInfoFrom(this, x, y);
            }
        }
    }
}

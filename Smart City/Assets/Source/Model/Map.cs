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
          *taille : uint (par défaut elle est de 6)
          Constructeur de la classe 
        */
        /// <summary>
        /// Constructeur de Map
        /// </summary>
        /// <param name="size"></param>
        public Map(uint size = 6){
            this.buildings = new Building[size, size];
            fillMapRandom1();
            observers = new List<MapObserver>();
        }

        /*
      *fillMapEmpty : fonction
      *Paramètre : 
      Permet de remplir la Map qui est vide
       */
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


        /* fillMapRandom1 : fonction 
       Permet de générer la carte d'une manière aléatoire
       */
        /// <summary>
        /// Permet de générer une carte de manière aléatoire
        /// </summary>
        public void fillMapRandom1(){
            int size = buildings.GetLength(0);
            System.Random rand = new System.Random();
            for(int i = 0;i < size;i++){
                for(int j = 0;j < size;j++){
                    buildings[i,j] = new Building(BuildType.Empty);
                }
            }

            for(int i=0;i<size/2;i++){
                int x = rand.Next(size/(size/2))+i*2;
                int y = rand.Next(size/(size/2))+i*2;
                buildings[x,y] = Building.createTransport();
            }
        }


        /* fillMapRandNoSeedEqualChance : fonction 
        Permet de générer une carte de manière aléatoire 
        */
        /// <summary>
        /// Permet de générer une carte de manière aléatoire
        /// </summary>
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


        /*
        *getUpgradeCostAt : fonction : uint
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet d'accéder au coût d'amélioration d'une case ayant comme coordonnées celles passées en paramètres. 
        */
        /// <summary>
        /// Permet de récupérer le coût d'amélioration à la case ayant comme coordonnées les entiers passés en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>Le coût d'amélioration d'une case</returns>
        public uint getUpgradeCostAt(uint posx, uint posy){
            return buildings[posx, posy].getUpgradeCost();
        }


        /*
        *getBuyCostAt : fonction : uint
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet d'accéder au coût d'achat d'une case ayant comme coordonnées celles passées en paramètres. 
       */
        /// <summary>
        /// Permet de récupérer le coût d'achat de la case ayant comme coordonnées les paramètres de la fonction
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>Le coût d'achat d'une case</returns>
        public uint getBuyCostAt(uint posx, uint posy){
            return buildings[posx,posy].getBuyCost();
        }

        /*
        *getTypeAt : fonction : BuildType
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet d'accéder au type de bâtiment d'une case ayant comme coordonnées celles passées en paramètres. 
       */
        /// <summary>
        /// Permet de récupérer le type de bâtiment de la case ayant comme coordonées les paramètres de la fonction
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>le type de bâtiment</returns>
        public BuildType getTypeAt(uint posx, uint posy){
            return buildings[posx,posy].type;
        }


        /*
        *getUnderMaxLevel : fonction : bool
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet de vérifier si le niveau d'amélioration est inférieur à 6
       */
        /// <summary>
        /// Permet de vérifier si le niveau d'amélioration est inférieur à 6
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>retourne vrai si le niveau est inférieur à 6, sinon faux</returns>
        public bool getUnderMaxLevel(uint posx, uint posy){
            return buildings[posx,posy].level < 6;
        }


        /*
        *buildAt : fonction
        *Paramètres : 
        * type : BuildType, type de bâtiment
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet de construire un bâtiment de type passé en paramètre dans la case ayant comme coordonnées celles passées en paramètres. 
       */   
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



        /*
        *buildAtNoMalus : fonction
        *Paramètres : 
        * type : BuildType, type de bâtiment
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet de construire un bâtiment de type passé en paramètre dans la case ayant comme coordonnées celles passées en paramètres sans malus.
       */
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


        /*
        *destroyAt : fonction
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet de détruire un bâtiment dans la case ayant comme coordonnées celles passées en paramètres. 
       */
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


        /*
        * UpgradeAt  : fonction
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet d'améliorer le bâtiment dans la case ayant comme coordonnées celles passées en paramètres. 
       */
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




        /*
        *setDecree : fonction
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet de mettre un décret sur la case dont les coordonnées sont passées en paramètres
       */
        /// <summary>
        /// Permet de mettre un décret de la case dont les coordonnées sont passées en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        public void setDecree(uint posx, uint posy){
            decree[0] = posx;
            decree[1] = posy;
        }



        /*
        *getDecree : fonction : bool
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet de vérifier si un décret existe sur la case dont les coordonnées sont passées en paramètres
       */
        /// <summary>
        /// Permet de vérifier s'il y a un décret
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>retourne vrai s'il en existe un, sinon faux</returns>
        public bool getDecree(uint posx, uint posy){
            return (decree[0] == posx) && (decree[1] == posy);
        }

        /*
        *getDecreePos : fonction : int[]
        Obtient les coordonnees du decret
        */
        /// <summary>
        /// Obtient la position du decret sur la carte
        /// </summary>
        /// <returns>retourne un tableau d'entier contenant les coordonnées x et y</returns>
        public uint[] getDecreePos(){
            return decree;
        }



        /*
        *setDecree : fonction
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet de mettre un décret sur la case dont les coordonnées sont passées en paramètres. 
       */
        /// <summary>
        /// Permet de mettre en place le décret
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        public void setBoost(uint posx, uint posy){
            boost[0] = posx;
            boost[1] = posy;
        }



        /*
        *getBoost : fonction : bool
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet de vérifier si un boost est placé sur la case dont les coordonnées sont passées en paramètres.
       */
        /// <summary>
        /// Permet de vérifier si un boost est placé sur la case dont les coordonnées sont passées en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>retourne vrai si un boost existe, sinon faux</returns>
        public bool getBoost(uint posx, uint posy){
            return (boost[0] == posx) && (boost[1] == posy);
        }

        /*
        *getBoostPos : fonction : int[]
        Permet d'obtenir la position du boost
       */
        /// <summary>
        /// Permet d'obtenir la position du boost
        /// </summary>
        /// <returns>retourne un tableau d'entier contenant les coordonnées x et y</returns>
        public uint[] getBoostPos(){
            return boost;
        }


        /*
        *getIncomeAt : fonction : uint
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet de récupérer les revenus générés par le bâtiment dans la case ayant comme coordonnées celles passées en paramètres. 
       */
        /// <summary>
        /// Permet de récupérer les bénéfices du bâtiment dont les coordonnées de sa case sont passées en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>retourne les revenus générés par le bâtiment sur la case dont les coordonnées sont passées en paramètres.</returns>
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



        /*
        *getIncomeAfterUpgradeAt : fonction : uint
        *Paramètres : 
        * posx : uint, coordonnée 
        * posy : uint : coordonnée
        Permet de construire un bâtiment de type passé en paramètre dans la case ayant comme coordonnées celles passées en paramètres. 
       */
        /// <summary>
        /// Permet de récupérer les bénéfices du bâtiment après son amélioration dont les coordonnées de sa case sont passées en paramètres
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <returns>retourne les revenus générés par le bâtiment dans la case dont les cooordonnées sont passées en paramètres après son amélioration.</returns>
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



        /*
        *isAdjacentToTransport : fonction : bool
        *Paramètres : 
        * x : uint, coordonnée 
        * y : uint : coordonnée
        Permet de vérifier si la case passée en paramètre est proche d'une case contenant un transport 
       */
        /// <summary>
        /// Permet de vérifier si la case passée en paramètre est proche d'une case contenant un transport
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>retourne vrai si c'est le cas, sinon faux</returns>
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


        /*
        *getIncomeFromType : fonction : uint
        *Paramètres : 
        * type : BuildType : type de bâtiment
        Permet de récupérer les revenus de tous les bâtiments d'un type donné
        */
        /// <summary>
        /// Permet de récupérer les revenus de tous les bâtiments d'un type donné
        /// </summary>
        /// <param name="type"></param>
        /// <returns>les revenus des bâtiments ayant le même type que celui passé en paramètre</returns>
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


        /*
        *getScoreFromType : fonction : uint
        *Paramètres : 
        * type : BuildType : type de bâtiment
        Permet de récupérer le score généré par les bâtiments ayant le même type que celui passé en paramètre
       */
        /// <summary>
        /// Permet de récupérer le score de tous les bâtiments d'un type donné
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Le score généré par les bâtiments ayant le même type que celui passé en paramètre</returns>
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


        /*
        *addObserver : fonction
        *Paramètres : 
        * observer : MapObserver : Observeur de map
        Permet d'ajouter des observers
      */
        /// <summary>
        /// Permet d'ajouter des observers
        /// </summary>
        /// <param name="observer"></param>
        public void addObserver(MapObserver observer){
            this.observers.Add(observer);
        }

        /*
        *notifyObservers : fonction
        *Paramètres : 
        Permet de notifier les observers
        */
        /// <summary>
        /// Permet de notifier les observers
        /// </summary>
        public void notifyObservers(){
            foreach(MapObserver observer in observers){
                observer.reactTo(this);
            }
        }

        /*
        *notifyObserversPos : fonction
        *Paramètres : 
        * x : uint, coordonnée 
        * y : uint : coordonnée
        Permet de notifier les observers qui ont comme coordonnées celles passées en paramètres
        */
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


        /*
        *notifyObserversChange : fonction
        *Paramètres : 
        * x : uint, coordonnée 
        * y : uint : coordonnée
        Permet de notifier les observers qui ont comme coordonnées celles passées en paramètres.
       */
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

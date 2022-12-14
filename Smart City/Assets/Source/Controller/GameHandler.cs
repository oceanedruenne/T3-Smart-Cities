using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Source.Model;
using Source.View;
using JeuScript;

namespace Source.Controller
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private GameObject gameHandler;

        private uint turn;
        private uint turnLimit = 20;

        private Player activePlayer;
        private City playerCity;
        private Company playerCompany;
        public Map map;

        private PlayerObserver playerObserver;
        private MapObserver mapObserver;

        public int posx;
        public int posy;
        public Tile currTile = null;
        public Tile BoostTile = null;
        public Tile DecreeTile = null;

        void Start(){
            startNewGame();
            StartCoroutine(LateStart(0.2f));
        }

        IEnumerator LateStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            map.notifyObservers();
            activePlayer.setScore(map);
            activePlayer.notifyObservers();
        }

        public void startNewGame(bool city = true){
            playerCity = new City();
            playerCompany = new Company();
            map = new Map();
            if(city){
                activePlayer = playerCity;
            }
            else {
                activePlayer = playerCompany;
            }
            turn = 1;

            playerObserver = new PlayerObserver();

            playerCity.addObserver(playerObserver);
            playerCompany.addObserver(playerObserver);

            mapObserver = new MapObserver(map, this);

            map.addObserver(mapObserver);

            resetSelectedTile();
        }

        public void nextTurn(){
            if(turn++ > turnLimit){
                endTurn();
                return;
            }
            if(turn > 1){
                activePlayer.addIncome(map);
            }
            activePlayer.setScore(map);
            if(activePlayer.isCity()){
                activePlayer = playerCompany;
            }
            else {
                activePlayer = playerCity;
            }
            resetSelectedTile();
            turn++;

            activePlayer.notifyObservers();
        }

        public void endTurn(){
            activePlayer.addIncome(map);
            activePlayer.setScore(map);

            //Ajouter les actions de fin de jeu
            Debug.Log("Fin du jeu");
        }

        public void selectTile(int posx, int posy, Tile tile){
            this.posx = posx;
            this.posy = posy;
            this.currTile = tile;
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }

        public void resetSelectedTile(){
            posx = -1;
            posy = -1;
            if(currTile!=null){
                currTile.deselect();
                currTile = null;
            }
            mapObserver.hideInfo(this.activePlayer.isCity());
        }

        public void buySelected(){
            activePlayer.Buy(map, (uint)posx, (uint)posy);
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }

        public void upgradeSelected(){
            activePlayer.Upgrade(map, (uint)posx, (uint)posy);
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }

        public void setPowerSelected(){
            activePlayer.specialsUse(map, (uint)posx, (uint)posy);
            if(activePlayer.isCity()){
                if(DecreeTile != null){
                    DecreeTile.unDecree();
                }
                DecreeTile = currTile;
                currTile.Decree();
            }
            else{
                if(BoostTile != null){
                    BoostTile.unBoost();
                }
                BoostTile = currTile;
                currTile.Boost();
            }
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }
    }
}

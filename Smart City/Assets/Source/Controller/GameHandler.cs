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

        private Player activePlayer;
        private City playerCity;
        private Company playerCompany;
        private Map map;

        private PlayerObserver playerObserver;
        private MapObserver mapObserver;

        private int posx;
        private int posy;
        public Tile currTile = null;

        void Start(){
            startNewGame();
            StartCoroutine(LateStart(0.2f));
        }

        IEnumerator LateStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            map.notifyObservers();
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


            posx = -1;
            posy = -1;

            playerObserver = new PlayerObserver();

            playerCity.addObserver(playerObserver);
            playerCompany.addObserver(playerObserver);

            mapObserver = new MapObserver(map, this);

            map.addObserver(mapObserver);
        }

        public void nextTurn(){
            activePlayer.addIncome(map);
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
        }

        public void buySelected(){
            activePlayer.Buy(map, (uint)posx, (uint)posy);
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }

        public void upgradeSelected(){
            activePlayer.Upgrade(map, (uint)posx, (uint)posy);
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }
    }
}

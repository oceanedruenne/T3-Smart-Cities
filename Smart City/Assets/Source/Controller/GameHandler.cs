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

        private AudioController audioController;

        /// <summary>
        /// Nouvelle partie
        /// </summary>
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
        /// <summary>
        /// Démarrer une nouvelle partie
        /// </summary>
        /// <param name="city"></param>
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

            audioController = FindObjectOfType<AudioController>();

            map.addObserver(mapObserver);

            resetSelectedTile();
        }

        /// <summary>
        /// Tour suivant
        /// </summary>
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

        /// <summary>
        /// Fin du tout
        /// </summary>
        public void endTurn(){
            activePlayer.addIncome(map);
            activePlayer.setScore(map);

            //Ajouter les actions de fin de jeu
            Debug.Log("Fin du jeu");
        }

        /// <summary>
        /// Permet de connaître la case selectionnée
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <param name="tile"></param>
        public void selectTile(int posx, int posy, Tile tile){
            this.posx = posx;
            this.posy = posy;
            this.currTile = tile;
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }

        /// <summary>
        /// Permet de décliquer sur la case selectionnée
        /// </summary>
        public void resetSelectedTile(){
            posx = -1;
            posy = -1;
            if(currTile!=null){
                currTile.deselect();
                currTile = null;
            }
            mapObserver.hideInfo(this.activePlayer.isCity());
        }

        /// <summary>
        /// Permet d'acheter la case selectionnée
        /// </summary>
        public void buySelected(){
            if(activePlayer.Buy(map, (uint)posx, (uint)posy))
            {
                audioController.playMoney();
            }
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }

        /// <summary>
        /// Permet d'améliorer la case selectionnée
        /// </summary>
        public void upgradeSelected(){
            activePlayer.Upgrade(map, (uint)posx, (uint)posy);
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }

        /// <summary>
        /// Permet de mettre en place le pouvoir choisi
        /// </summary>
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

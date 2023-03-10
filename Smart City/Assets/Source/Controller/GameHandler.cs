using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Source.Model;
using Source.View;
using JeuScript;
using UnityEngine.SceneManagement;
using TMPro;

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

        public uint playerCityScore;
        public uint playerCityMoney;
        public uint playerCompanyScore;
        public uint playerCompanyMoney;

        public static uint getTourActuel;
        public static uint getNbTours;
        

        public int posx;
        public int posy;
        public Tile currTile = null;
        public Tile BoostTile = null;
        public Tile DecreeTile = null;

        public GameObject panel;
        public GameObject avatar;
        public TextMeshProUGUI TurnText;

        private AudioController audioController;

        public GameObject panellFinTour;

        public GameObject panelCity;
        public GameObject panelCompany;

        /// <summary>
        /// Nouvelle partie
        /// </summary>
        void Start()
        {
            startNewGame();
            StartCoroutine(LateStart(0.2f));
            panel = GameObject.Find("PanelHaut");
            avatar = GameObject.Find("AvatarConstant");
            TurnText = GameObject.Find("TurnText").GetComponent<TextMeshProUGUI>();
            TurnText.text = turn + " / " + turnLimit;
            panellFinTour = GameObject.Find("PanelFinTour");
        }

        IEnumerator LateStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            map.notifyObservers();
            playerCity.setScore(map);
            playerCompany.setScore(map);
            activePlayer.notifyObservers();
        }

        /* startNewGame : fonction 
         Parametre : city : booo : true 
         Cette fonction permet de demarrer une nouvelle partie 
        */
        /// <summary>
        /// Demarrer une nouvelle partie
        /// </summary>
        /// <param name="city"></param>
        public void startNewGame(bool city = true)
        {
            playerCity = new City();
            playerCompany = new Company();
            map = new Map();
            if (city)
            {
                activePlayer = playerCity;
                panelCompany.SetActive(false);
                panelCity.SetActive(true);
            }
            else
            {
                activePlayer = playerCompany;
                panelCity.SetActive(false);
                panelCompany.SetActive(true);
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

        /* nextTurn : fonction 
        Cette fonction permet de passer au tour suivant
       */
        /// <summary>
        /// Tour suivant
        /// </summary>
        public void nextTurn()
        {   
            activePlayer.earn = 0;
            if (turn+1 > turnLimit)
            {
                endTurn();
                return;
            }
            if (turn > 0)
            {
                activePlayer.addIncome(map);
            }
            if (activePlayer.isCity())
            {
                activePlayer = playerCompany;
                panelCity.SetActive(false);
                panelCompany.SetActive(true);
            }
            else
            {
                activePlayer = playerCity;
                panelCompany.SetActive(false);
                panelCity.SetActive(true);
            }
            resetSelectedTile();
            turn++;
            TurnText.text = turn + " / " + turnLimit;
            audioController.playNextTurn();
            activePlayer.notifyObservers();
            activePlayer.setScore(map);
            if(turn>2){
                StartCoroutine(IbeginTurn());
            }
        }

        IEnumerator IbeginTurn()
        {  
            activePlayer.notifyObserversBeginRound(activePlayer, map);
            yield return new WaitForSeconds(2.5f);
            activePlayer.earn = 0;
            panellFinTour.SetActive(false);
        }

        /*endTurn  : fonction 
        Cette fonction permet de terminer le jeu
        */
        /// <summary>
        /// Fin du tour
        /// </summary>
        public void endTurn()
        {
            activePlayer.addIncome(map);
            activePlayer.setScore(map);

            //Ajouter les actions de fin de jeu
            SceneManager.LoadScene("EndScene");
            playerCityMoney = playerCity.getMoney();
            playerCityScore = playerCity.getScore();
            playerCompanyMoney = playerCompany.getMoney();
            playerCompanyScore = playerCompany.getScore();

            PlayerPrefs.SetInt("playerCityMoney", (int)playerCityMoney);
            PlayerPrefs.SetInt("playerCityScore", (int)playerCityScore);
            PlayerPrefs.SetInt("playerCompanyMoney", (int)playerCompanyMoney);
            PlayerPrefs.SetInt("playerCompanyScore", (int)playerCompanyScore);
        }


        /* selectTile : fonction 
         * Parametres : posx : int, posy : int, tile : Tile
         Permet de connaitre la case selectionnee.
        */
        /// <summary>
        /// Permet de connaitre la case selectionnee
        /// </summary>
        /// <param name="posx"></param>
        /// <param name="posy"></param>
        /// <param name="tile"></param>
        public void selectTile(int posx, int posy, Tile tile)
        {
            this.posx = posx;
            this.posy = posy;
            this.currTile = tile;
            audioController.playSelected();
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }

        /* resetSelectedTile : fonction 
        Permet de decliquer la case selectionnee.
       */
        /// <summary>
        /// Permet de decliquer la case selectionnee
        /// </summary>
        public void resetSelectedTile()
        {
            posx = -1;
            posy = -1;
            if (currTile != null)
            {
                currTile.deselect();
                currTile = null;
            }
            mapObserver.hideInfo(this.activePlayer.isCity());
        }

        /* buySelected : fonction 
        Permet d'acheter la case selectionnee
        */
        /// <summary>
        /// Permet d'acheter la case selectionnee
        /// </summary>
        public void buySelected()
        {
            if (activePlayer.Buy(map, (uint)posx, (uint)posy))
            {
                audioController.playMoney();
            }
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }

        /* upgradeSelected : fonction 
        Permet d'ameliorer la case selectionnee
        */
        /// <summary>
        /// Permet d'ameliorer la case selectionnee
        /// </summary>
        public void upgradeSelected()
        {
            if (activePlayer.Upgrade(map, (uint)posx, (uint)posy))
            {
                audioController.playUpgrade();
            }
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }


        /* setPowerSelected : fonction 
        Permet de mettre en place le pouvoir choisi
        */
        /// <summary>
        /// Permet de mettre en place le pouvoir choisi
        /// </summary>
        public void setPowerSelected(){
            if(activePlayer.isCity() && map.getBoostPos()[0] == posx && map.getBoostPos()[1] == posy || !activePlayer.isCity() && map.getDecreePos()[0] == posx && map.getDecreePos()[1] == posy){
                return;
            }
            activePlayer.specialsUse(map, (uint)posx, (uint)posy);
            if (activePlayer.isCity())
            {

                audioController.playPowerBlock();

                if (DecreeTile != null)
                {
                    DecreeTile.unDecree();
                }
                DecreeTile = currTile;
                currTile.Decree();
            }
            else
            {

                audioController.playPowerMoney();

                if (BoostTile != null)
                {
                    BoostTile.unBoost();
                }
                BoostTile = currTile;
                currTile.Boost();
            }
            mapObserver.UpdateInfoFrom(map, (uint)posx, (uint)posy);
        }

        /*public void exitGame(){
            Debug.Log("Pas cod??");
        } */
    }
}

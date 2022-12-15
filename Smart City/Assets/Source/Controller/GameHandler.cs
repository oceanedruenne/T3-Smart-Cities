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

        /// <summary>
        /// Nouvelle partie
        /// </summary>
        void Start()
        {
            startNewGame();
            StartCoroutine(LateStart(0.2f));
            PlayerPrefs.SetInt("tourTotal",(int)this.turnLimit);
            panel = GameObject.Find("PanelHaut");
            avatar = GameObject.Find("AvatarConstant");
            TurnText = GameObject.Find("TurnText").GetComponent<TextMeshProUGUI>();
            TurnText.text = turn + " / " + turnLimit;
        }

        IEnumerator LateStart(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            map.notifyObservers();
            playerCity.setScore(map);
            playerCompany.setScore(map);
            activePlayer.notifyObservers();
        }

        IEnumerator IendRound()
        {
            yield return new WaitForSeconds(0);
            activePlayer.earnAfterRound(map);
            activePlayer.notifyObserversEndRound();
            yield return new WaitForSeconds(3);
            activePlayer.earn = 0;
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
            }
            else
            {
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

        /* nextTurn : fonction 
        Cette fonction permet de passer au tour suivant
       */
        /// <summary>
        /// Tour suivant
        /// </summary>
        public void nextTurn()
        {
            if (turn+1 > turnLimit)
            {
                endTurn();
                return;
            }
            if (turn > 1)
            {
                activePlayer.addIncome(map);
            }
            if (activePlayer.isCity())
            {
                StartCoroutine(IendRound());
                activePlayer = playerCompany;
            }
            else
            {
                StartCoroutine(IendRound());
                activePlayer = playerCity;
            }
            resetSelectedTile();
            turn++;
            TurnText.text = turn + " / " + turnLimit;
            StartCoroutine(InewRound());
        }

        IEnumerator InewRound()
        {
            yield return new WaitForSeconds(1.5f);
            audioController.playNextTurn();
            activePlayer.notifyObservers();
            activePlayer.setScore(map);
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

        public void exitGame(){
            Debug.Log("Pas codé");
        }
    }
}

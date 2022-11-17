using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Source.Model;
using Source.View;

namespace Source.Controller
{
    public class GameHandler : MonoBehaviour
    {
        private uint turn = 1;
        private City playerCity;
        private Company playerCompany;
        private Map map;
        private PlayerObserver playerObserver;
        private MapObserver mapObserver;

        void Start(){
            startNewGame();
        }

        public void startNewGame(){
            playerCity = new City();
            playerCompany = new Company();
            map = new Map();

            playerObserver = new PlayerObserver();

            playerCity.addObserver(playerObserver);
            playerCompany.addObserver(playerObserver);

            mapObserver = new MapObserver();

            map.addObserver(mapObserver);
        }
    }
}

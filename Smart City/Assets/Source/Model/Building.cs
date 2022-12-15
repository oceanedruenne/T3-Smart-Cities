using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Model
{

    // CLASSE BUILDING
    public class Building
    {
        private static int BASE_COST = 1000; //Prix de base d'un bâtiment
        private static int BASE_INCOME = 300;
        public static float MALUS_INCREASE = 0.2f;
        /*
        Ce qui suit s'appelle un getter 
        (il existe aussi des setters, si jamais :
        https://learn.microsoft.com/fr-fr/dotnet/csharp/programming-guide/classes-and-structs/using-properties)
        Ceci revient au même que faire une fonction getBuyMalus renvoyant buyMalus, 
        sauf que il sera possible de faire directement un Building.buyMalus 
        au lieu de Building.getBuyMalus
        */
        private BuildType _type;
        public BuildType type => _type;
        private float _buyMalus;
        public float buyMalus => _buyMalus;
        private int _level;
        public int level => _level;

        /*
            *Building : Constructeur : Building
            *Paramètres : 
            *Type : BuildType : type du bâtiment crée (voir l'enumération BuildType)
            malus : double : buyMalus du bâtiment, égal au buyMalus du bâtiments précedent + MALUS_INCREASE sinon 1.0
            level : uint : Niveau du bâtiment, égal au bâtiment précédent sinon 0
            Constructeur de la classe 
        */
        /// <summary>
        /// Constructeur de Building
        /// </summary>
        /// <param name="type"></param>
        /// <param name="malus"></param>
        /// <param name="level"></param>
        public Building(BuildType type, float malus = 1f, int level = 0){
            this._type = type;
            this._buyMalus = malus;
            this._level = level;
        }

        /*
           *Transport : Fonction : Building
           *Construit le bâtiment transport qui ne sera pas achetable et qui va donner des bonus aux bâtiments autour
           *Variables locales : 
           *transport : Building : le bâtiment
       */

        /// <summary>
        /// Permet de créer des transports
        /// </summary>
        /// <returns>Un building de type transport</returns>
        public static Building createTransport() {
            Building transport = new Building(BuildType.Transport, 0, 0);
            return transport;
        }

        /*
            *getIncome : fonction : uint
            *Paramètres : Aucun
            Retourne le revenu rapporté par le bâtiment, qui correspond à la formule suivante :
            BASE_INCOME * ((level * 0,5) + 1) cad 
            niveau 0 : BASE_INCOME * 1 (= (0 / 2) + 1 )
            Niveau 1 : BASE_INCOME * 1,5
            Niveau 2 : BASE_INCOME * 2
            etc...
        */
        /// <summary>
        /// Permet de récupérer les revenus du bâtiment
        /// </summary>
        /// <returns>Les revenus du bâtiment</returns>
        public uint getIncome(){
            return (uint)(BASE_INCOME * ((level / 1f) + 1));
        }

        /*
            *getIncomeAfterUpgrade : fonction : uint
            *Paramètres : Aucun
            Retourne le revenu rapporté par le bâtiment au niveau + 1
        */
        /// <summary>
        /// Permet de récupérer les bénéfices du bâtiment après son amélioration
        /// </summary>
        /// <returns>Les bénéfices du bâtiment après son amélioration</returns>
        public uint getIncomeAfterUpgrade(){
            return (uint)(BASE_INCOME * (((level+1) / 2f) + 1));
        }

        /*
            *getCost : fonction : uint
            *Paramètres : Aucun
            Retourne le coût du bâtiment à l'achat, selon la formule suivante :
            BASE_COST * ((0,5 * level) + 1) * buyMalus cad
            Niveau 0 + buyMalus 1.0 : BASE_COST * 1 * 1
            Niveau 1 + buyMalus 1.0 : BASE_COST
        */

        /// <summary>
        /// Permet de récupérer le coût d'achat d'un bâtiment
        /// </summary>
        /// <returns>Le coût d'achat d'un bâtiment</returns>
        public uint getBuyCost(){
            return (uint)(BASE_COST * ((level / 2) + 1) * buyMalus);
        }

        /*
            *getUpgradeCost : fonction : uint
            *Paramètres : Aucun 
            *Permet de récupérer le coût d'amélioration d'un bâtiment
        */

        /// <summary>
        /// Permet de récupérer le coût d'amélioration d'un bâtiment
        /// </summary>
        /// <returns>le coût d'amélioration d'un bâtiment</returns>
        public uint getUpgradeCost(){
            return (uint)(BASE_COST * ((level / 2) + 1));
        }
    }
}

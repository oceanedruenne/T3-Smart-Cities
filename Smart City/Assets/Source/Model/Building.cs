using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Model
{
    public class Building : MonoBehaviour
    {
        private static uint BASE_COST = 1000;
        private static uint BASE_INCOME = 200;
        private static float MALUS_INCREASE = 0.2;
        private BuildType type
        {
            get => type;
        }
        /*
        Ce qui suit s'appelle un getter 
        (il existe aussi des setter, si jamais :
        https://learn.microsoft.com/fr-fr/dotnet/csharp/programming-guide/classes-and-structs/using-properties)
        Ceci reviens au même que faire une fonction getBuyMalus renvoyant buyMalus, 
        sauf que il sera possible de faire directement un Building.buyMalus 
        au lieu de Building.getBuyMalus
        */
        private float buyMalus
        {
            get => buyMalus;
        }
        private uint level
        {
            get => level;
        }

        /*
            *Building : Constructeur : Building
            *Paramètre :
            type : BuildType : type du bâtiment crée (voir l'enumération BuildType)
            malus : float : buyMalus du bâtiment, égal au buyMalus du bâtiments précedent + MALUS_INCREASE sinon 1.0
            level : uint : Niveau du bâtiment, égal au bâtiment précédent sinon 0
            *Keskecé : 
            Constructeur de la classe
            *Varibales locales :
        */
        public Building(BuildType type, float malus = 0.8, uint level = 0){
            this.type = type;
            this.buyMalus = malus + MALUS_INCREASE;
            this.level = level;
        }

        /*
            *getIncome : fonction : uint
            *Paramètre : Aucun
            *Keskecé : 
            Retourne le revenu rapporté par le bâtiment, qui correspond à la formule suivante :
            BASE_INCOME * ((level * 0,5) + 1) cad 
            niveau 0 : BASE_INCOME * 1
            Niveau 1 : BASE_INCOME * 1,5
            Niveau 2 : BASE_INCOME * 2
            etc...
            *Varibales locales :
        */
        public uint getIncome(){
            return BASE_INCOME * ((level * 0,5) + 1);
        }

        /*
            *getCost : fonction : uint
            *Paramètre : 
            *Keskecé :
            Retourne le coût du bâtiment à l'achat, selon la formule suivante :
            BASE_COST * ((0,5 * level) + 1) * buyMalus cad
            Niveau 0 + buyMalus 1.0 : BASE_COST * 1 * 1
            Niveau 1 + buyMalus 1.0 : BASE_COST
            *Varibales locales :

        */
        public uint getBuyCost(){
            return BASE_COST * ((0,5 * level) + 1) * buyMalus;
        }

        public uint getUpgradeCost(){
            return BASE_COST * ((0,5 * level) + 1);
        }

        public void upgrade(){
            this.level++;
        }

        /*
            *schéma : fonction/proc : typeretour
            *Paramètre : 
            *Keskecé :
            *Varibales locales :
        */
    }
}

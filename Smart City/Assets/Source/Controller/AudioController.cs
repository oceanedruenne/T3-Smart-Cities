using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Source.Controller
{
    public class AudioController : MonoBehaviour
    {

        public AudioClip[] sounds; //Gestion du son à jouer
        public float volume; //Gestion du volume
        public float pitch; //Gestion de la vitesse du morceau

        private AudioSource source; //Composant qui reçoit les paramètres ci-dessus

        private void Awake()
        {
            source = GetComponent<AudioSource>();

            volume = 1f;
            pitch = 1f;
        }

        void Start()
        {
            source.playOnAwake = false;

            //Initialisation
            source.clip = sounds[0];
            source.volume = volume;
            source.pitch = pitch;
        }

        /* playMoney : fonction 
        Cette fonction permet de jouer un son spécifique pour l'argent*/
        /// <summary>
        /// Cette fonction permet de jouer un son spécifique pour l'argent
        /// </summary>
        public void playMoney()
        {
            source.clip = sounds[0];
            source.Play();
        }

        /* playUpgrade : fonction 
        Cette fonction permet de jouer un son spécifique pour les améliorations*/
        /// <summary>
        /// Cette fonction permet de jouer un son spécifique pour les améliorations
        /// </summary>
        public void playUpgrade()
        {
            source.clip = sounds[1];
            source.Play();
        }

        /* playPowerMoney : fonction 
        Cette fonction permet de jouer un son spécifique pour le pouvoir de la GAFAM*/
        /// <summary>
        /// Cette fonction permet de jouer un son pour le pouvoir de la GAFAM
        /// </summary>
        public void playPowerMoney()
        {
            source.clip = sounds[2];
            source.Play();
        }

        /* playPowerBlock : fonction 
        Cette fonction permet de jouer un son spécifique pour le pouvoir de la municipalité*/
        /// <summary>
        /// Cette fonction permet de jouer un son pour le pouvoir de la municipalité
        /// </summary>
        public void playPowerBlock()
        {
            source.clip = sounds[3];
            source.Play();
        }

        /* playNextTurn : fonction 
        Cette fonction permet de jouer un son spécifique de passage au tour suivant*/
        /// <summary>
        /// Cette fonction permet de jouer un son pour le passage au tour suivant
        /// </summary>
        public void playNextTurn()
        {
            source.clip = sounds[4];
            source.Play();
        }

        /* playSelected : fonction 
        Cette fonction permet de jouer un son spécifique à la sélection d'un bâtiment*/
        /// <summary>
        /// Cette fonction permet de jouer un son à la sélection d'un bâtiment
        /// </summary>
        public void playSelected()
        {
            source.clip = sounds[5];
            source.Play();
        }

    }
}

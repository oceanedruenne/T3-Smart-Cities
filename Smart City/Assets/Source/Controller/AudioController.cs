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
        public void playMoney()
        {
            source.clip = sounds[0];
            source.Play();
        }

        /* playUpgrade : fonction 
        Cette fonction permet de jouer un son spécifique pour les améliorations*/
        public void playUpgrade()
        {
            source.clip = sounds[1];
            source.Play();
        }

        /* playPowerMoney : fonction 
        Cette fonction permet de jouer un son spécifique pour le pouvoir de la GAFAM*/
        public void playPowerMoney()
        {
            source.clip = sounds[2];
            source.Play();
        }

        /* playPowerBlock : fonction 
        Cette fonction permet de jouer un son spécifique pour le pouvoir de la municipalité*/
        public void playPowerBlock()
        {
            source.clip = sounds[3];
            source.Play();
        }

        /* playNextTurn : fonction 
        Cette fonction permet de jouer un son spécifique de passage au tour suivant*/
        public void playNextTurn()
        {
            source.clip = sounds[4];
            source.Play();
        }

        /* playSelected : fonction 
        Cette fonction permet de jouer un son spécifique à la sélection d'un bâtiment*/
        public void playSelected()
        {
            source.clip = sounds[5];
            source.Play();
        }

    }
}

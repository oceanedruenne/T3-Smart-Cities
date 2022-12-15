using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Source.Controller
{
    public class AudioController : MonoBehaviour
    {

        public AudioClip sound; //Gestion du son à jouer
        public float volume; //Gestion du volume
        public float pitch; //Gestion de la vitesse du morceau

        private AudioSource source; //Composant qui reçoit les paramètres ci-dessus

        private void Awake()
        {
            gameObject.AddComponent<AudioSource>();
            source = GetComponent<AudioSource>();

            volume = 0.5f;
            pitch = 1f;
        }

        // Start is called before the first frame update
        void Start()
        {
            source.clip = sound;
            source.volume = volume;
            source.pitch = pitch;
        }

        // Update is called once per frame
        void Update()
        {

        }

        /* playMoney : fonction 
        Cette fonction permet de jouer une musique spécifique pour l'argent*/
        public void playMoney()
        {
            source.Play();
        }
    }
}

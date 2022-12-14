using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptButtonCommencer : MonoBehaviour
{
    /* BtnNextScene() : 
       Cette fonction permet de passer à la scène suivante lorsqu'on appuie sur le bouton Commencer*/
   public void BtnNextScene()
   {
       SceneManager.LoadScene("SignUp");
   }
}

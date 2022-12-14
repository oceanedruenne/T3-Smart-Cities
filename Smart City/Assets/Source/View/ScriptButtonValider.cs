using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptButtonValider : MonoBehaviour
{
    /* BtnNextScene() : 
    Cette fonction permet de passer à la scène suivante lorsqu'on appuie sur le bouton "Valider"*/
   public void BtnNextScene()
   {  
    SceneManager.LoadScene("GameScene");
   }
}

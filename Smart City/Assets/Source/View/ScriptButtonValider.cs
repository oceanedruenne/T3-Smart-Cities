using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptButtonValider : MonoBehaviour
{

    /*
    *BtnNextScene : fonction : 
    Permet de passer à la scène principale du jeu
    */
    /// <summary>
    /// Permet de passer à la scène principale du jeu
    /// </summary>
    public void BtnNextScene()
   {  
    SceneManager.LoadScene("GameScene");
   }
}

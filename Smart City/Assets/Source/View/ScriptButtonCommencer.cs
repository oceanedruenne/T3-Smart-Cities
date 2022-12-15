using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptButtonCommencer : MonoBehaviour
{

    /*
    *BtnNextScene : fonction 
    Permet de passer à la scène suivante 
    */
    /// <summary>
    /// Permet de passer à la scène d'inscription
    /// </summary>
    public void BtnNextScene()
   {
       SceneManager.LoadScene("SignUp");
   }
}

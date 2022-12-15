using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScriptButtonQuitter : MonoBehaviour
{


    /*
    *ExitGame : fonction
    Permet de quitter le jeu
    */
    /// <summary>
    /// Permet de quitter le jeu
    /// </summary>
    public void ExitGame() 
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Quit !");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnRecommencer : MonoBehaviour
{
    /*
    *startAgain : fonction
    Permet de revenir à la scène principale
    */
    /// <summary>
    /// Permet de revenir à la scène principale
    /// </summary>
    public void startAgain() 
    {
       SceneManager.LoadScene("MainMenu");
    }
}

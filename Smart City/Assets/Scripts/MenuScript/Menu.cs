using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    //charge la scene 1 
    public void loadScene1()
    {
        SceneManager.LoadScene(1);
    }

    //quitter le jeu
    public void exitGame()
    {
        Application.Quit();
    }

    //ouvre l'url du git
    public void loadCredit()
    {
        Application.OpenURL("https://git.unistra.fr/les-chatons-mignons-uwu/t3-smart-city");
    }
}

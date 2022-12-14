using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Charge la première scène
    public void loadScene1()
    {
        SceneManager.LoadScene(1);
    }

    // Permet de quitter le jeu
    public void exitGame()
    {
        Application.Quit();
    }

    // Ouvre l'URL du Git
    public void loadCredit()
    {
        Application.OpenURL("https://git.unistra.fr/les-chatons-mignons-uwu/t3-smart-city");
    }
}

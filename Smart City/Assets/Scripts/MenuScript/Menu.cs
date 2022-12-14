using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   /// <summary>
   /// Permet de charger la première scène
   /// </summary>
    public void loadScene1()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Permet de quitter le jeu
    /// </summary>
    public void exitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Permet d'ouvrir l'URL de notre git
    /// </summary>
    public void loadCredit()
    {
        Application.OpenURL("https://git.unistra.fr/les-chatons-mignons-uwu/t3-smart-city");
    }
}

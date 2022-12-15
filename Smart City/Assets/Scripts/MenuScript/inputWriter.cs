using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inputWriter : MonoBehaviour
{
    public static string cityName;

   /// <summary>
   /// On récupère le nom de ville écrit par le joueur dans la scène "Sign Up" et on l'affiche dans la scène "GameScene"
   /// </summary>
    void Start()
    {   
        GameObject panel = GameObject.Find("PanelHaut");
        TextMeshProUGUI NameText = GameObject.Find("NameText").GetComponent<TextMeshProUGUI>();
        NameText.text = PlayerPrefs.GetString("cityName");
    }

    void Update()
    {
        GameObject panel = GameObject.Find("PanelHaut");
        TextMeshProUGUI NameText = GameObject.Find("NameText").GetComponent<TextMeshProUGUI>();
        NameText.text = PlayerPrefs.GetString("cityName");
    }
}

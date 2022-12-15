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
        TextMeshProUGUI VilleText = GameObject.Find("NameText").GetComponent<TextMeshProUGUI>();
        VilleText.text = PlayerPrefs.GetString("cityName");
    }

    void Update()
    {
       
    }
}

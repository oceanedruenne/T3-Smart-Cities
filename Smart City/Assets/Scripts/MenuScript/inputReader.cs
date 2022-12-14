using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inputReader : MonoBehaviour
{
    public static string cityName;

    void Start()
    {
       
      
    }
    
    // On lit le nom de la ville entré par l'utilisateur
    void Update()
    {
        TextMeshProUGUI textInter = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
        cityName = textInter.text;
        PlayerPrefs.SetString("cityName",cityName);
    } 
}

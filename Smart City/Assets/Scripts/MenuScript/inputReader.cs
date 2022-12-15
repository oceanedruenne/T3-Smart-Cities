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
       InputField inputInter = GameObject.Find("inputCityName").GetComponent<InputField>();
       TextMeshProUGUI textInter = GameObject.Find("Text").GetComponent<TextMeshProUGUI>(); 
       cityName = textInter.text;
       PlayerPrefs.SetString("cityName",cityName);     
    }
    
    /// <summary>
    /// Permet de lire le nom de la ville entré par l'utilisateur dans l'input de la scène "Sign Up"
    /// </summary>
    void Update()
    {
        TextMeshProUGUI textInter = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
        cityName = textInter.text;
        PlayerPrefs.SetString("cityName",cityName);
    } 
}

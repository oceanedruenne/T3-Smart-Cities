using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inputWriter : MonoBehaviour
{
    public static string cityName;

    // On récupère le nom de la ville entré par l'utilisateur dans la prédécente scène et on l'affiche dans celle-ci
    void Start()
    {   
        TextMeshProUGUI VilleText = GameObject.Find("NomText").GetComponent<TextMeshProUGUI>();
        VilleText.text = PlayerPrefs.GetString("cityName");
    }

    void Update()
    {
       
    }
}

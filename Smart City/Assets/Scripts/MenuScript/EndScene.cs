using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScene : MonoBehaviour
{
    
    public static uint playerCityScore;
    public static uint playerCityMoney;

    public static uint playerCompanyScore;
    public static uint playerCompanyMoney;

    // Permet d'afficher les informations dans la scène finale
    void Update()
    {
        TextMeshProUGUI scoreMunicipalite = GameObject.Find("txtScoreMunicipalite").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI argentMunicipalite = GameObject.Find("txtArgentMunicipalite").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI scoreGafam = GameObject.Find("txtScoreGafam").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI argentGafam = GameObject.Find("txtArgentGafam").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI nomVille = GameObject.Find("txtNameCity").GetComponent<TextMeshProUGUI>(); 

        scoreMunicipalite.text = PlayerPrefs.GetInt("playerCityScore").ToString();
        argentMunicipalite.text = PlayerPrefs.GetInt("playerCityMoney").ToString();
        scoreGafam.text = PlayerPrefs.GetInt("playerCompanyScore").ToString();
        argentGafam.text = PlayerPrefs.GetInt("playerCompanyMoney").ToString();
        nomVille.text = PlayerPrefs.GetString("cityName");  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayCityName : MonoBehaviour
{
    public TextMeshPro CityName;
    void Start()
    {
        CityName = GameObject.Find("CityName").GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        CityName.text = PlayerPrefs.GetString("cityName");
    }
    
}

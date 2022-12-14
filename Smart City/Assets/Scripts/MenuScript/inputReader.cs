using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inputReader : MonoBehaviour
{
    public static string cityName;

    // Start is called before the first frame update
    void Start()
    {
       
      
    }

    
    void Update()
    {
        TextMeshProUGUI textInter = GameObject.Find("Text").GetComponent<TextMeshProUGUI>();
        cityName = textInter.text;
        PlayerPrefs.SetString("cityName",cityName);
    } 
}

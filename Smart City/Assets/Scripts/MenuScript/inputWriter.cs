using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class inputWriter : MonoBehaviour
{
    public static string cityName;

    // Start is called before the first frame update
    void Start()
    {   
        TextMeshProUGUI VilleText = GameObject.Find("NomText").GetComponent<TextMeshProUGUI>();
        VilleText.text = PlayerPrefs.GetString("cityName");
    }

    void Update()
    {
       
    }
}

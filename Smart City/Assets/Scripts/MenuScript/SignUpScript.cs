using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SignUpScript : MonoBehaviour
{

    void OnDisable()
    {
       InputField txt_Input = GameObject.Find("inputCityName").GetComponent<InputField>();
       PlayerPrefs.SetString("cityName",txt_Input.text);
    }
}

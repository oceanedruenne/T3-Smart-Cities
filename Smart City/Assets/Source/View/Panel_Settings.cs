using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Panel_Settings : MonoBehaviour
{
    public Sprite sound_on;
    public Sprite sound_off;
    bool isOn = true;
    bool isActive = false;

    Image image; //Image sur le bouton

    void Start()
    {
        image = GameObject.Find("ButtonMusic").GetComponent<Image>();
    }

    public void clickUpdate()
    {
        if (isOn)
        {
            image.sprite = sound_off;
            isOn = false;
        }
        else
        {
            image.sprite = sound_on;
            isOn = true;
        }
    }

    public void activePanel()
    {
        this.gameObject.SetActive(isActive = !isActive);
    }

}

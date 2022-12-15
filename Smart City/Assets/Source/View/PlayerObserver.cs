using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Source.View;
using Source.Model;
using TMPro;

public class PlayerObserver : ScriptableObject, Observer
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI earnText;
    public GameObject panellFinTour;


    /*
     *PlayerObserver : Constructeur : PlayerObserver
     Constructeur de la classe 
    */
    /// <summary>
    /// Permet de trouver les informations du joueur : son score et son argent
    /// </summary>
    public PlayerObserver()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.Find("NameText").GetComponent<TextMeshProUGUI>();

        earnText = GameObject.Find("EarnText").GetComponent<TextMeshProUGUI>();
        panellFinTour = GameObject.Find("PanelFinTour");
    }

    /*
    *reactTo : fonction 
    *Paramètre : player : Player : le joueur
    *Permet de donner au joueur passé en paramètre de l'argent et un score
   */
    /// <summary>
    /// Permet de donner au joueur passé en paramètre de l'argent et un score
    /// </summary>
    /// <param name="player"></param>
    public void reactTo(Player player)
    {
        panellFinTour.SetActive(false);
        scoreText.text = player.score.ToString();
        moneyText.text = player.money.ToString();
        nameText.text = player.name.ToString();
        Debug.Log("player observer " + player.earn.ToString());
    }

    public void reactToEndRound(Player player)
    {
        panellFinTour.SetActive(true);
        earnText.text = player.name + " + " + player.earn.ToString();
    }
}

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

    /// <summary>
    /// Permet de trouver les informations du joueur : son score et son argent
    /// </summary>
    public PlayerObserver()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
    }
    /// <summary>
    /// Permet de donner au joueur passé en paramètre de l'argent et un score
    /// </summary>
    /// <param name="player"></param>
    public void reactTo(Player player){
        scoreText.text = player.score.ToString();
        moneyText.text = player.money.ToString();
    }
}

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

    // Permet de trouver les informations du joueur : son score et son argent
    public PlayerObserver()
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        moneyText = GameObject.Find("Argent").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
    }
    // Permet de donner au score et à l'argent du joueur les valeurs correspondantes en passant en paramètre le joueur
    public void reactTo(Player player){
        scoreText.text = "Score : " + player.score.ToString();
        moneyText.text = "Argent : " + player.money.ToString();
        nameText.text = player.name.ToString();

        scoreText.text = player.score.ToString();
        moneyText.text = player.money.ToString();
    }
}

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

    public PlayerObserver()
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        moneyText = GameObject.Find("Argent").GetComponent<TextMeshProUGUI>();
        nameText = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
    }
    public void reactTo(Player player){
<<<<<<< HEAD
        scoreText.text = "Score : " + player.score.ToString();
        moneyText.text = "Argent : " + player.money.ToString();
        nameText.text = player.name.ToString();

=======
        scoreText.text = player.score.ToString();
        moneyText.text = player.money.ToString();
>>>>>>> 9a80909868451f2a8e91949f1cb7c9db4de0bb2c
    }
}

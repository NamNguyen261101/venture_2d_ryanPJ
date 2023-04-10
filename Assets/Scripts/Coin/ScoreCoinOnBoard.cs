using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCoinOnBoard : MonoBehaviour
{
    public static ScoreCoinOnBoard instance;

    private Text scoreText;
    private int score;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        scoreText.text = "X " + " " + score.ToString();
    }
}

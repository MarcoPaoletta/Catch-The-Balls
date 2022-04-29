using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;

    void Update()
    {
        scoreText.text = GameManager.score.ToString();
        livesText.text = "Lives: " + GameManager.lives.ToString();
    }
}

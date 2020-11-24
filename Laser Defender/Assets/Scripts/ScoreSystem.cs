using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void AddPoints(int points)
    {
        int currentScore = int.Parse(scoreText.text.ToString());
        currentScore += points;
        scoreText.SetText(currentScore.ToString());
    }
}
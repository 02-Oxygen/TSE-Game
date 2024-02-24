using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;
    public int score;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
    }

    public void SetScore(int correct, int incorrect, float time)
    {
        score+= (int)Math.Ceiling(correct * ((2000 / (time + 0.1)) / (((incorrect / 2) + 1))));
        UpdateScoretext();
    }

    public void UpdateScoretext()
    {
        UIManager.Instance.UpdateText(UIManager.Instance.scoretext, "Score: " + score.ToString());
    }
}

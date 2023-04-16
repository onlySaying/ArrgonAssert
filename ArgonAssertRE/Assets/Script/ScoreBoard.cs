using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int score;
    TMP_Text scoretxt;

    private void Start()
    {
        scoretxt = GetComponent<TMP_Text>();
        scoretxt.text = "start";
    }

    public void IncreaseScore(int value)
    {
        score += value;
        scoretxt.text = score.ToString();
    }
}

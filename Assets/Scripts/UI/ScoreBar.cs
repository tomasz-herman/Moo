using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    public TMP_Text scoreText;
    public void SetScore(int value)
    {
        scoreText.text = value.ToString();
    }
}

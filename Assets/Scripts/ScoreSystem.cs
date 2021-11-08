using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public HeadUpDisplay hud;
    private int score = 0;
    void Start()
    {
        UpdateHud();
    }

    public void AddScore(int points = 1)
    {
        score += points;
        UpdateHud();
    }
    private void UpdateHud()
    {
        hud.SetScore(score);
    }
}

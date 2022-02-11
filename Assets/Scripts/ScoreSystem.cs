using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public HeadUpDisplay hud;
    private float score = 0;

    void Start()
    {
        UpdateHud();
    }

    public void AddScore(float points)
    {
        score += points;
        UpdateHud();
    }
    private void UpdateHud()
    {
        hud.scoreBar.SetScore(IntScore);
    }

    public int IntScore { get { return Mathf.CeilToInt(score); } }
    public float Score { get { return score; } }
}

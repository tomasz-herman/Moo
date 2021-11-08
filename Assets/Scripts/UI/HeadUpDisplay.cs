using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeadUpDisplay : MonoBehaviour
{
    public TMP_Text scoreText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScore(int value)
    {
        scoreText.text = value.ToString();
    }
}

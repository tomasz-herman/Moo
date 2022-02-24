using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    private TMP_Text text;

    void Start()
    {
        text = GetComponentInChildren<TMP_Text>(true);

        text.gameObject.SetActive(ApplicationData.Debug);
        ApplicationData.DebugChanged.AddListener((debug) => text.gameObject.SetActive(debug));
    }      

    void Update()
    {
        text.text = Mathf.RoundToInt(1 / Time.deltaTime).ToString();
    }
}

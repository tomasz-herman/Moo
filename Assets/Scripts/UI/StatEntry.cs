using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatEntry : MonoBehaviour
{
    public TMP_Text nameText, valueText;
    
    public void SetName(string name) { nameText.text = name; }
    public void SetValue(string value) { valueText.text = value; }
    public void SetNameColor(Color color) { nameText.color = color; }
    public void SetValueColor(Color color) { valueText.color = color; }
}

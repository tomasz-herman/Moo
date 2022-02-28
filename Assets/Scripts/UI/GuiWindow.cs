using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiWindow : MonoBehaviour
{
    public GameObject overlay;

    public virtual void Awake()
    {
        overlay = gameObject;
    }
}

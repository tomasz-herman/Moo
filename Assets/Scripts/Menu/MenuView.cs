using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public MainMenu Menu { get; private set; }

    public virtual void Awake()
    {
        Menu = GetComponentInParent<MainMenu>();
    }

    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

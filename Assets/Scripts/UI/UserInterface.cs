using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    public HeadUpDisplay hud;
    void Start()
    {
        hud = GetComponentInChildren<HeadUpDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

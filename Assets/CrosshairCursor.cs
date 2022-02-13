using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairCursor : MonoBehaviour
{
    public Image image;
    void Update()
    {
        if (Time.timeScale == 0)
        {
            Cursor.visible = true;            
            image.enabled = false;
        }
        else
        {
            Cursor.visible = false;
            image.enabled = true;
        }
        transform.position = Input.mousePosition;
    }

    private void OnDestroy()
    {
        Cursor.visible = true;
    }
}

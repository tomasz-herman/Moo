using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : MonoBehaviour
{
    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}

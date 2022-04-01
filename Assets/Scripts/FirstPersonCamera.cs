using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public GameObject followed;
    public Vector3 shift = new Vector3(0, 0, 0);
    void Start()
    {
        this.transform.parent = followed.transform;
        this.transform.localPosition = shift;
    }
}

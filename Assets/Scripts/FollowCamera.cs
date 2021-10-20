using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject followed;
    public Vector3 toTarget = new Vector3(0, -5, -5);
    void Start()
    {
        
    }

    void Update()
    {
        if(followed != null)
        {
            gameObject.transform.position = followed.transform.position - toTarget;
            gameObject.transform.LookAt(followed.transform);
        }
    }
}

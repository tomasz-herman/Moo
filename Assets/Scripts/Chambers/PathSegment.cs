using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment : MonoBehaviour
{
    private Renderer renderer;

    private void Awake()
    {
        renderer = gameObject.GetComponentInChildren<Renderer>();
    }

    public void SetMaterial(Material material)
    {
        renderer.material = material;
    }
}

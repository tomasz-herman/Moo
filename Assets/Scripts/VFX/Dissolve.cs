using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] Material DissolveMaterial;
    [SerializeField] float time = 1f;
    List<Renderer> renderers = new List<Renderer>();
    System.Action finished;
    float DissolveTime = 0;
    bool dissolve = false;
    private void Start()
    {
        renderers = gameObject.GetComponentsInChildren<Renderer>().ToList();
    }

    private void Update()
    {
        if (dissolve)
        {
            if (Time.time < DissolveTime)
            {
                foreach (var item in renderers)
                {
                    item.material.SetFloat("_Step", (DissolveTime - Time.time) / time);
                }
            }
            else
            {
                finished.Invoke();
                dissolve = false;
            }

        }
    }

    public void StartDissolve(System.Action Finished)
    {
        finished = Finished;
        DissolveTime = Time.time + time;
        dissolve = true;
        foreach (var item in renderers)
        {
            item.material = DissolveMaterial;
        }
    }
}

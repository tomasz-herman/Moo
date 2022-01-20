using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterEffectScript : MonoBehaviour
{
    private Animator animator;
    private GameObject SpawnedObject;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void AddSpawnedObject(GameObject gameObject)
    {
        SpawnedObject = gameObject;
        SpawnedObject.SetActive(false);
    }

    public void Spawn()
    {
        SpawnedObject.SetActive(true);
    }

    public void DestroyTeleportEffect()
    {
        animator.SetBool("Animate", false);
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterEffectScript : MonoBehaviour
{
    private Animator animator;
    private GameObject SpawnedObject;
    private static GameObject TeleportEffect = null;

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

    public static void CreateTeleporterForEntity(GameObject entity, float teleporterScale)
    {
        if (TeleportEffect == null)
        {
            TeleportEffect = Resources.Load<GameObject>("TeleporterEffect");
        }
        var tele = GameObject.Instantiate(TeleportEffect, entity.transform.position, Quaternion.identity).GetComponent<TeleporterEffectScript>();
        tele.gameObject.transform.localScale *= teleporterScale;
        tele.AddSpawnedObject(entity);
    }
}

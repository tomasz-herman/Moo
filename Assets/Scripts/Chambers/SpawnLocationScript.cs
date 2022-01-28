using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocationScript : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position+new Vector3(0f,2f,0f), new Vector3(2, 4, 2));
    }
}

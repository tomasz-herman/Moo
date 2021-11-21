using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    Animator animation;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.IsWalking)
        {
            animation.SetBool("isWalking", true);
        }
        else
            animation.SetBool("isWalking", false);
    }
}

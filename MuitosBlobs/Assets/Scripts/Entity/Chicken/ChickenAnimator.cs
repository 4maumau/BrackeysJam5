using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{
    private Animator animator;
    private Chicken jumpScript;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        jumpScript = GetComponent<Chicken>();
    }

   
    void Update()
    {
        animator.SetFloat("VerticalVel", jumpScript.verticalVelocity);
        animator.SetBool("isGrounded", jumpScript.isGrounded);
    }
}

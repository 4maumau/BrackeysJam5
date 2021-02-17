using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimator : MonoBehaviour
{
    private Animator animator;
    private FakeHeight jumpScript;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        jumpScript = GetComponent<FakeHeight>();
    }

   
    void Update()
    {
        animator.SetFloat("VerticalVel", jumpScript.verticalVelocity);
        animator.SetBool("isGrounded", jumpScript.isGrounded);
    }
}

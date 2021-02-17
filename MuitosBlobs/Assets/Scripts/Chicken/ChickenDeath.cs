using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDeath : MonoBehaviour
{
    private Animator animator;
    private bool isDead;
    private FakeHeight jumpScript;

    private void Start()
    {
        animator = GetComponent<Animator>();
        jumpScript = GetComponentInParent<FakeHeight>();
    }

    public void OnDeath()
    {
        jumpScript.enabled = false;
        animator.Play("ChickenDeath");
        
    }

    public void OnAnimationEnd()
    {
        Destroy(transform.root.gameObject);
    }

}

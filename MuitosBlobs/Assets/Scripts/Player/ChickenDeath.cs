using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDeath : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnDeath()
    {
        animator.Play("ChickenDeath");
    }

    public void OnAnimationEnd()
    {
        Destroy(transform.root.gameObject);
        print("should be gone");
    }

}

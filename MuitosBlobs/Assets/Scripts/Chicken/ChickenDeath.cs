using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDeath : MonoBehaviour
{
    private Animator animator;
    private FakeHeight jumpScript;

    private MultipleTargetCamera multiCamera;

    private void Start()
    {
        animator = GetComponent<Animator>();
        jumpScript = GetComponentInParent<FakeHeight>();

        multiCamera = FindObjectOfType<MultipleTargetCamera>();

        multiCamera.targets.Add(transform.root);
    }

    public void OnDeath()
    {
        jumpScript.enabled = false;
        multiCamera.targets.Remove(transform.root);
        animator.Play("ChickenDeath");
        
    }

    public void OnAnimationEnd()
    {
        Destroy(transform.root.gameObject);
    }

}

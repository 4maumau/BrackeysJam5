using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDeath : MonoBehaviour
{
    private Animator animator;
    private Chicken jumpScript;

    private MultipleTargetCamera multiCamera;


    private void Start()
    {
        animator = GetComponent<Animator>();
        jumpScript = GetComponentInParent<Chicken>();


        multiCamera = FindObjectOfType<MultipleTargetCamera>();

        multiCamera.targets.Add(transform.root);
    }

    public void OnDeath()
    {
        jumpScript.enabled = false;
        multiCamera.targets.Remove(transform.root);
        animator.Play("ChickenDeath");
        AudioManager.instance.PlaySound("ChickenDeath");
        if (Random.value > 0.8) AudioManager.instance.PlaySound("ChickenScream");

        ScreenShakeController.instance.AddTrauma(.1f);

    }

    public void OnAnimationEnd()
    {
        Destroy(transform.root.gameObject);
        GameManager.instance.ChickenDeath();
    }

}

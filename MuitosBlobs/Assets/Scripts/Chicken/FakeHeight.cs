using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeHeight : MonoBehaviour
{
    [Header ("Transforms")]

    [SerializeField] private Transform transformObject;
    [SerializeField] private Transform transformBody;
    [SerializeField] private Transform transformShadow;


    [Header("Velocities")]
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float gravity;
    public float verticalVelocity;
    private Vector2 groundVelocity;

    [Header("Animation")]
    private Animator animator;
    private EntityAnimator entityAnimator;

    public bool isGrounded = true;


    private LifeManager lifeManager;
    private MousePosition mousePositionScript;

    private void Start()
    {
        mousePositionScript = FindObjectOfType<MousePosition>();
        mousePositionScript.OnMouseClick.AddListener(MouseClick);

        animator = GetComponentInChildren<Animator>();
        entityAnimator = GetComponentInChildren<EntityAnimator>();

        lifeManager = GetComponentInChildren<LifeManager>();
        lifeManager.SetInvincibility(1f);
    }

    private void Update()
    {
        UpdatePosition();
        CheckGroundHit();
        
    }

    private void UpdatePosition()
    {
       
        if (!isGrounded)
        {
            transformObject.position += (Vector3)groundVelocity * Time.deltaTime;

            verticalVelocity += gravity * Time.deltaTime;
            transformBody.position += new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
        }
    }

    public void JumpTo(Vector2 _groundVelocity, float _verticalVelocity)
    {
        //entityAnimator.DoSqueeze(0.5f, 1.4f, 0.1f);

        groundVelocity = _groundVelocity; 
        verticalVelocity = _verticalVelocity;
    }

    
    private void MouseClick(Vector2 mousePos)
    {
        if (isGrounded)
        {
            isGrounded = false;
            //animator.Play("ChickenJump");

            Vector2 jumpDirection = mousePos - (Vector2)transformObject.position;
            float distanceMouseToChicken = Vector2.Distance(mousePos, transformObject.position); // algum jeito de pegar essa distancia e regular com o jumpVelocity


            jumpDirection.Normalize();

            JumpTo(jumpDirection * moveSpeed, jumpVelocity);
        }
        else return;
    }

    private void CheckGroundHit()
    {
        if (transformBody.position.y < transformObject.position.y && !isGrounded)
        {
            isGrounded = true;
            //entityAnimator.DoSqueeze(1.34f, 0.6f, 0.06f);
            animator.Play("ChickenGroundHit");
        }
    }

    

    
}

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
    private float verticalVelocity;
    private Vector2 groundVelocity;

    [Header("Animation")]
    private bool isGrounded;
    private Animator animator;
    [SerializeField] private GameObject chickenSprite;
    private ChickenAnimator chickenAnimator;
    

    private MousePosition mousePositionScript;

   
    




    private void Start()
    {
        mousePositionScript = FindObjectOfType<MousePosition>();
        mousePositionScript.OnMouseClick.AddListener(MouseClick);

        animator = GetComponentInChildren<Animator>();
        chickenAnimator = GetComponentInChildren<ChickenAnimator>();
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
        chickenAnimator.DoSqueeze(0.3f, 1.6f, 0.1f);

        groundVelocity = _groundVelocity; 
        verticalVelocity = _verticalVelocity;
    }

    
    private void MouseClick(Vector2 mousePos)
    {
        isGrounded = false;
        //animator.Play("ChickenJump");

        Vector2 jumpDirection =  mousePos - (Vector2) transformObject.position;
        float distanceMouseToChicken = Vector2.Distance(mousePos, transformObject.position); // algum jeito de pegar essa distancia e regular com o jumpVelocity
        
        print("distance mouse - position: " + distanceMouseToChicken);
        print("new jump velocity: " + distanceMouseToChicken * jumpVelocity);

        jumpDirection.Normalize();

        JumpTo(jumpDirection * moveSpeed, jumpVelocity);
    }

    private void CheckGroundHit()
    {
        if (transformBody.position.y < transformObject.position.y && !isGrounded)
        {
            isGrounded = true;
            chickenAnimator.DoSqueeze(1.45f, 0.45f, 0.08f);
            animator.Play("ChickenIdle");
        }
    }

    

    
}

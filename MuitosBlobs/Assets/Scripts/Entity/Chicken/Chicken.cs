using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
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

    private ParticleSystem groundHitParticle;
        

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        entityAnimator = GetComponentInChildren<EntityAnimator>();

        lifeManager = GetComponentInChildren<LifeManager>();
        lifeManager.SetInvincibility(1f);


        groundHitParticle = GetComponentInChildren<ParticleSystem>();

        GameManager.instance.NewChicken();
        AudioManager.instance.PlaySound("ChickenSpawn");

        SetShootingMode();

    }

    private void Update()
    {
        UpdatePosition();
        CheckGroundHit();



        MouseJump();
        TouchJump();
       
    }

    public void JumpTo(Vector2 _groundVelocity, float _verticalVelocity)
    {
        isGrounded = false;

        groundVelocity = _groundVelocity;
        verticalVelocity = _verticalVelocity;

        //characterAudio.PlaySound("Jump");
    }

    void MouseJump()
    {
        if (Input.GetMouseButton(0))
        {
            JumpCall(Input.mousePosition);
        }
    }

    private void TouchJump()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            JumpCall(touch.position);
        }
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

    public void Knocback(Vector2 _xVelocity, float _yVelocity)
    {
        isGrounded = false;
        JumpTo(_xVelocity, _yVelocity);
    }

   

    
    private void JumpCall(Vector2 mousePos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        if (isGrounded)
        {

            Vector2 jumpDirection = worldPos - (Vector2)transformObject.position;
            //float distanceMouseToChicken = Vector2.Distance(mousePos, transformObject.position); // algum jeito de pegar essa distancia e regular com o jumpVelocity


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
            ScreenShakeController.instance.AddTrauma(0.005f);

            AudioManager.instance.PlaySound("ChickenLand");
            animator.Play("ChickenGroundHit");
            groundHitParticle.Emit(1);

        }
    }

    void SetShootingMode()
    {
        //if (PlayerPrefs.GetString("ShootingMode") == "Auto")
        //{
        GetComponent<Shooting>().enabled = true;
        GetComponent<ChickenShooting>().enabled = false;
        //}
        
    }

    
}

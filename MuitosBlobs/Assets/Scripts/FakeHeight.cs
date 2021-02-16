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

    private bool isGrounded;

    private MousePosition mousePosition;

    private Vector2 target;
    public float maxDistanceDelta = 5;


    private void Start()
    {
        mousePosition = FindObjectOfType<MousePosition>();
        mousePosition.OnMouseClick.AddListener(MouseClick);
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
            //transformObject.position += (Vector3)groundVelocity * Time.deltaTime;
            transformObject.position = Vector2.MoveTowards(transformObject.position, target, maxDistanceDelta * Time.deltaTime);

            verticalVelocity += gravity * Time.deltaTime;
            transformBody.position += new Vector3(0, verticalVelocity, 0) * Time.deltaTime;
        }


    }

    public void JumpTo(Vector2 _groundVelocity, float _verticalVelocity)
    {
        groundVelocity = _groundVelocity; 
        verticalVelocity = _verticalVelocity;

        
    }

    
    private void MouseClick(Vector2 mousePos)
    {
        isGrounded = false;
        

        Vector2 jumpDirection =  mousePos - (Vector2) transformObject.position;
        float distanceMouseToChicken = Vector2.Distance(mousePos, transformObject.position); // algum jeito de pegar essa distancia e regular com o jumpVelocity
        
        print("distance mouse - position: " + distanceMouseToChicken);
        print("new jump velocity: " + distanceMouseToChicken * jumpVelocity);

        jumpDirection.Normalize();


        //JumpTo(jumpDirection * moveSpeed, jumpVelocity * distanceMouseToChicken);
        target = mousePos;
        verticalVelocity = jumpVelocity;
        
    }

    private void CheckGroundHit()
    {
        if (transformBody.position.y < transformObject.position.y && !isGrounded)
        {
            isGrounded = true;

        }
    }
   
}

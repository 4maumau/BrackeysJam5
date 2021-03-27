using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour, IPooledObject
{
    [Header("Transforms")]

    [SerializeField] private Transform transformObject;
    [SerializeField] private Transform transformBody;
    [SerializeField] private Transform transformShadow;

    [Header("Velocities")]

    //debug only
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float xVelocity;

    [SerializeField] private float gravity;

    private float verticalVelocity;
    private Vector2 groundVelocity = Vector2.right;

    private bool isGrounded;

    public void OnObjectSpawn()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Initialize(groundVelocity * xVelocity, jumpVelocity);
        }

        UpdatePosition();
        CheckGroundHit();
    }

    public void Initialize(Vector2 _groundVelocity, float _verticalVelocity)
    {

        isGrounded = false;
        groundVelocity = _groundVelocity;
        verticalVelocity = _verticalVelocity;
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

    private void CheckGroundHit()
    {
        if (transformBody.position.y < transformObject.position.y && !isGrounded)
        {
            isGrounded = true;
            groundVelocity = Vector2.right;
        }
    }
}

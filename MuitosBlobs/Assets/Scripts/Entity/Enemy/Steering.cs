using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float steerStrength = 5;
    public Vector2 target;

    Vector2 position;
    Vector2 velocity;
    Vector2 desiredDirection;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKey(KeyCode.Space)) // if !isBombEnemy && distanceToTarget > 5f
            desiredDirection = (position - (Vector2)target).normalized;
        else
            desiredDirection = ((Vector2)target - position).normalized;

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity) * steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector2.ClampMagnitude(velocity + acceleration * Time.deltaTime, maxSpeed);
        position += velocity * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, position, maxSpeed * Time.deltaTime);

    }
}

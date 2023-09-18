using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DynamicMover : Mover
{
    public Rigidbody2D rb;
    public float maxSpeed;
    public float maxAcceleration;
    public float targetRadius;
    public float slowRadius;
    public float dt = 0.1f;

    private Vector2 currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = default;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Move(Vector3 steeringDirection, Transform target)
    {
        Vector2 directionToTarget = target.position - transform.position;
        float distance = directionToTarget.magnitude;

        if (distance <= targetRadius)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float targetSpeed = 0;
        Vector2 targetVelocity = default;

        if (distance > slowRadius)
        {
            targetSpeed = maxSpeed;
        }
        else
        {
            targetSpeed = maxSpeed * (distance / slowRadius);

        }

        targetVelocity = steeringDirection.normalized * targetSpeed;

        Vector2 acceleration = targetVelocity - rb.velocity;
        acceleration /= dt;

        if (acceleration.magnitude > maxAcceleration)
        {
            acceleration = acceleration.normalized * maxAcceleration;
        }

        currentVelocity = new Vector2(
            currentVelocity.x + acceleration.x * Time.deltaTime,
            currentVelocity.y + acceleration.y * Time.deltaTime);

        FaceTarget(steeringDirection);

        rb.velocity = currentVelocity;
    }

    public void FaceTarget(Vector3 velocity)
    {
        if (velocity.magnitude > 0)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicMover : Mover
{
    public float maxSpeed;
    public float targetRadius;

    private void Start()
    {
        
    }

    public override void Move(Vector3 steeringDirection, Transform target)
    {
        Vector2 direction = target.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= targetRadius)
        {
            return;
        }

        Vector2 velocity = steeringDirection.normalized * (distance / targetRadius);

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        FaceTarget(velocity);

        float newX = transform.position.x + velocity.x * Time.deltaTime;
        float newY = transform.position.y + velocity.y * Time.deltaTime;

        transform.position = new Vector2(newX, newY);
    }

    private void FaceTarget(Vector3 velocity)
    {
        if (velocity.magnitude > 0)
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}

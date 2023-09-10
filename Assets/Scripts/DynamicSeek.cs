using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSeek : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject[] Targets;
    public float maxSpeed;
    public float maxAcceleration;
    public float targetRadius;
    public float slowRadius;
    public float dt = 0.1f;

    private Vector2 currentVelocity;

    private Queue<GameObject> targetQueue;
    private GameObject currentTarget;

    // Start is called before the first frame update
    void Start()
    {
        currentVelocity = default;
        targetQueue = new Queue<GameObject>(Targets);
        currentTarget = targetQueue.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        Steer();
    }

    public void Steer()
    {
        Vector2 direction = currentTarget.transform.position - transform.position;
        float distance = direction.magnitude;

        if (distance <= targetRadius)
        {
            targetQueue.Enqueue(currentTarget);
            currentTarget = targetQueue.Dequeue();
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

        targetVelocity = direction.normalized * targetSpeed;        

        Vector2 acceleration = targetVelocity - rb.velocity;
        acceleration /= dt;

        if (acceleration.magnitude > maxAcceleration)
        {
            acceleration = acceleration.normalized * maxAcceleration;
        }

        currentVelocity = new Vector2(
            currentVelocity.x + acceleration.x * Time.deltaTime,
            currentVelocity.y + acceleration.y * Time.deltaTime);

        FaceTarget(currentVelocity);

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

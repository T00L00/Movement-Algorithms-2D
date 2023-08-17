using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSeek : MonoBehaviour
{
    public GameObject[] Targets;
    public float maxAcceleration;

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
        if (currentTarget != null
            && Vector2.Distance(currentTarget.transform.position, transform.position) >= 0.1f)
        {
            Vector2 acceleration = currentTarget.transform.position - transform.position;
            acceleration.Normalize();
            acceleration *= maxAcceleration;

            currentVelocity = new Vector2(
                currentVelocity.x + acceleration.x * Time.deltaTime, 
                currentVelocity.y + acceleration.y * Time.deltaTime);

            FaceTarget(currentVelocity);

            float newX = transform.position.x + currentVelocity.x * Time.deltaTime;
            float newY = transform.position.y + currentVelocity.y * Time.deltaTime;

            transform.position = new Vector2(newX, newY);
            return;
        }

        targetQueue.Enqueue(currentTarget);
        currentTarget = targetQueue.Dequeue();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : MonoBehaviour
{
    public GameObject[] Targets;
    public float maxSpeed;

    private Queue<GameObject> targetQueue;
    private GameObject currentTarget;

    // Start is called before the first frame update
    void Start()
    {
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
            Vector2 velocity = currentTarget.transform.position - transform.position;
            velocity.Normalize();
            velocity *= maxSpeed;

            FaceTarget(velocity);

            float newX = transform.position.x + velocity.x * Time.deltaTime;
            float newY = transform.position.y + velocity.y * Time.deltaTime;

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

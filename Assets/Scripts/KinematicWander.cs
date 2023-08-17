using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicWander : MonoBehaviour
{
    public float MaxSpeed;
    public float MaxRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Steer();
    }

    public void Steer()
    {
        Vector2 velocity = MaxSpeed * transform.right;

        transform.Rotate(0, 0, RandomBinomial() * MaxRotation);

        float newX = transform.position.x + velocity.x * Time.deltaTime;
        float newY = transform.position.y + velocity.y * Time.deltaTime;

        transform.position = new Vector2(newX, newY);
    }

    public float RandomBinomial()
    {
        System.Random rand = new();
        return (float)(rand.NextDouble() - rand.NextDouble());
    }
}

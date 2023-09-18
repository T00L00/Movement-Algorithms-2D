using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidObstacleBehavior : SteeringBehavior
{
    [SerializeField] private float avoidanceRadius = 1f;

    private float[] currentWeights;

    private void Start()
    {
        Init(8);
    }

    public void Init(int numberOfDirections)
    {
        currentWeights = new float[numberOfDirections];
    }

    public override float[] ComputeWeights(SteeringData data)
    {
        for (int i = 0; i < currentWeights.Length; i++)
        {
            currentWeights[i] = 0f;
        }

        foreach (Collider2D obstacle in data.Obstacles)
        {
            Vector2 direction = obstacle.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distance = direction.magnitude;

            float weightByObstacle = 0f;
            if (distance <= avoidanceRadius)
            {
                weightByObstacle = 1f;
            }
            else
            {
                weightByObstacle = avoidanceRadius / distance;
            }            

            for (int i = 0; i < SteeringDirections.vectors.Count; i++)
            {
                float weightByDirection = Vector2.Dot(direction.normalized, SteeringDirections.vectors[i]);
                float netWeight = -weightByDirection * weightByObstacle;

                if (currentWeights[i] > netWeight)
                {
                    currentWeights[i] = netWeight;
                }
            }
        }

        return currentWeights;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < currentWeights.Length; i++)
            {
                Gizmos.DrawRay(transform.position, SteeringDirections.vectors[i] * currentWeights[i]);
            }
        }
    }
}

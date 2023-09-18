using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleDetector : Detector
{
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private LayerMask layerMask;

    private Collider2D[] obstacles;

    public override SteeringData Detect(SteeringData data)
    {
        obstacles = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerMask);

        foreach (Collider2D o in obstacles)
        {
            data.Obstacles.Add(o);
        }

        return data;
    }
}

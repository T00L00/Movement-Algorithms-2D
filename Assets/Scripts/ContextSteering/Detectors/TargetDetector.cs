using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetDetector : Detector
{
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private LayerMask layerMask;

    private Collider2D target;
    private Collider2D[] colliders;

    public Collider2D Target => target;

    public override SteeringData Detect(SteeringData data)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layerMask);

        if (colliders.Length == 0)
        {
            data.Target = null;
            return data;
        }

        Collider2D nearest = colliders.OrderBy(t => Vector3.Distance(t.transform.position, transform.position)).First();

        if (data.Target != nearest.transform)
        {
            data.Target = nearest.transform;
        }

        return data;
    }
}

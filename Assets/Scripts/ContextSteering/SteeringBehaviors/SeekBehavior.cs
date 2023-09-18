using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehavior : SteeringBehavior
{
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
        Vector3 directionToTarget = data.Target.position - transform.position;
        for (int i = 0; i < SteeringDirections.vectors.Count; i++)
        {
            float weight = Vector3.Dot(directionToTarget.normalized, SteeringDirections.vectors[i]);
            currentWeights[i] = weight;
        }

        return currentWeights;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < currentWeights.Length; i++)
            {
                Gizmos.DrawRay(transform.position, SteeringDirections.vectors[i] * currentWeights[i]);
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextSteering : MonoBehaviour
{
    [SerializeField] private float detectionFrequency = 0.5f;
    [SerializeField] private Transform tip;
    [SerializeField] private Mover mover;
    [SerializeField] private Detector[] detectors;
    [SerializeField] private SteeringBehavior[] steeringBehaviors;

    private SteeringData steeringData;

    private float[] directionWeights = new float[8];
    private Vector3 steeringDirection;

    // Start is called before the first frame update
    void Start()
    {
        steeringData = new();
        steeringData.Obstacles = new();
        steeringDirection = transform.forward;

        InvokeRepeating("Detect", 0, detectionFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        if (steeringData.Target != null)
        {
            if (TargetInSight(steeringData.Target))
            {
                ResetDirectionWeights();
                foreach (SteeringBehavior s in steeringBehaviors)
                {
                    float[] weights = s.ComputeWeights(steeringData);
                    directionWeights = ElementWiseAdd(directionWeights, weights);
                }

                Vector3 outputDirection = default;
                for (int i = 0; i < SteeringDirections.vectors.Count; i++)
                {
                    outputDirection += SteeringDirections.vectors[i] * directionWeights[i];
                }
                outputDirection.Normalize();
                steeringDirection = outputDirection;
            }
        }
    }

    private void FixedUpdate()
    {
        if (steeringData.Target != null)
        {
            mover.Move(steeringDirection, steeringData.Target);
        }
    }

    private void Detect()
    {
        foreach (Detector d in detectors)
        {
            steeringData = d.Detect(steeringData);
        }
    }

    private void ResetDirectionWeights()
    {
        for (int i = 0; i < directionWeights.Length; i++)
        {
            directionWeights[i] = 0;
        }
    }

    private float[] ElementWiseAdd(float[] array, float[] other)
    {
        if (array.Length != other.Length)
        {
            throw new ArgumentException("Both arrays must have the same length.");
        }

        float[] result = new float[array.Length];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = Mathf.Clamp01(array[i] + other[i]);
        }
        return result;
    }

    private bool TargetInSight(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(tip.position, direction, distance);
        if (hit.collider != null)
        {
            if (hit.collider.transform == target) return true;
        }
        return false;
    }   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, steeringDirection);
    }
}

public static class SteeringDirections
{
    public static List<Vector3> vectors = new()
    {
        new Vector3(0,1,0).normalized,
        new Vector3(1,1,0).normalized,
        new Vector3(1,0,0).normalized,
        new Vector3(1,-1,0).normalized,
        new Vector3(0,-1,0).normalized,
        new Vector3(-1,-1,0).normalized,
        new Vector3(-1,0,0).normalized,
        new Vector3(-1,1,0).normalized
    };
}


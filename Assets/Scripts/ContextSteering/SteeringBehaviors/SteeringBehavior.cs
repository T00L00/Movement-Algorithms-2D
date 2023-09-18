using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SteeringBehavior : MonoBehaviour
{
    public abstract float[] ComputeWeights(SteeringData data);
}

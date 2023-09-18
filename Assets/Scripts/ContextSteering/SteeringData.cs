using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SteeringData
{
    public Transform Target;
    public HashSet<Collider2D> Obstacles;
}

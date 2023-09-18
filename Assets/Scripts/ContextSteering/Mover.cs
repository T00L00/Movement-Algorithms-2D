using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    public abstract void Move(Vector3 steeringDirection, Transform target);
}

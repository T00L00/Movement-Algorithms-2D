using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float Speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = default;
        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
        }

        transform.position += direction * Speed * Time.deltaTime;
    }
}

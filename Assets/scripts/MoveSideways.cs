using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSideways : MonoBehaviour
{

    Vector3 startPosition;

    void Start() {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        float xMovement = Mathf.Sin(Time.time);

        transform.position = startPosition + new Vector3(xMovement, 0, 0);
    }
}

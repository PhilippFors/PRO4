using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    float value = 1f;
    void Update()
    {
        transform.position += new Vector3(0, value, 0) * Time.deltaTime;
        if (transform.position.y <= -3f)
        {
            value = 1f;
        } else if(transform.position.y >=1f){
            value = -1f;
        }

    }
}

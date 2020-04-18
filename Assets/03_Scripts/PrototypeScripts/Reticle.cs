using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}

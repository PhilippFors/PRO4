using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    public float sensitivty;
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}

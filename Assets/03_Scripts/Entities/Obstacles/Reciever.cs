using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reciever : MonoBehaviour
{
    public bool occupied;
    public bool active;
    private void Start()
    {
        occupied = false;
        active = false;
    }
}

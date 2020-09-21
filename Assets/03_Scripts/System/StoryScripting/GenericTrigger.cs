﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericTrigger : MonoBehaviour
{
    public TriggerBehaviour behaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBody>())
        {
            behaviour.Execute(other, this);
            gameObject.SetActive(false);
        }
    }
}

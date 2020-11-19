using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
public class PlayerDetector : MonoBehaviour
{
    public PlayerBody player;

    void OnTriggerEnter(Collider other)
    {
        PlayerBody p = other.gameObject.GetComponent<PlayerBody>();
        if (p != null)
            player = p;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBody>())
            player = null;
    }
}

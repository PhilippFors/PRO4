using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBody>())
        {
            LevelEventSystem.instance.LevelExit();
            enabled = false;
        }
    }
}

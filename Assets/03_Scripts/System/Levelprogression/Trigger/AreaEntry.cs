using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntry : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            LevelEventSystem.instance.AreaEntry();
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}

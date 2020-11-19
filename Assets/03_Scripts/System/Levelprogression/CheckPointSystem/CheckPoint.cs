using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
public class CheckPoint : MonoBehaviour
{
    public bool isActive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBody>())
        {
            LevelEventSystem.instance.CheckPoinReached(this);
        }
    }
}

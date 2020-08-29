using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntry : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LeStart());
    }

    IEnumerator LeStart()
    {
        yield return new WaitForEndOfFrame();
        LevelEventSystem.instance.LevelEntry();
    }
}

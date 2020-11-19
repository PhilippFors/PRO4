﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
public class DestroyParticle : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy(){
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }

}

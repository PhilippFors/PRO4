﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            
            SaveManager.instance.Save();
            GetComponent<BoxCollider>().enabled = false;
        }

    }
}

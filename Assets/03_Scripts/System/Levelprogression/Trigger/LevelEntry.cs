using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEntry : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
       LevelEventSystem.instance.LevelEntry();
   }
}

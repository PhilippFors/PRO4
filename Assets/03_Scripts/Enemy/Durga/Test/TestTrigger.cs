using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    float baseDmg = 6.0f;
   private void OnTriggerEnter(Collider other)
   {
       GameObject obj = other.gameObject;
       if(obj.GetComponent<SuperClass>() != null){
           TesteEvent.instance.Activation(obj.GetComponent<SuperClass>(), baseDmg);
           
       }
   }
}

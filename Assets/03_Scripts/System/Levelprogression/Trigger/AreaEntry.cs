using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AreaEntry : MonoBehaviour
{
    public int AreaID;
    public bool disabled = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {

            LevelEventSystem.instance.AreaEntry();

            // SpawnManager.instance.areaStarted = true;
            DisableSelf();
        }
    }
    // private void Awake()
    // {
    //     gameObject.name = "_____Area " + AreaID + " ______";
    //     AreaID++;
    // }
    public void DisableSelf()
    {
        GetComponent<BoxCollider>().enabled = false;
        disabled = true;
    }
}

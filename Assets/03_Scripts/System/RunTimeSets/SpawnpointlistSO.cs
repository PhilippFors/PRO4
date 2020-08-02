using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SpawnpointList", menuName = "SpawnpointList")]
public class SpawnpointlistSO : ScriptableObject
{
    public List<SpawnpointID> list;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Author(mainAuthor = "Philipp Forstner")]

[CreateAssetMenu(fileName = "New SpawnpointList", menuName = "SpawnpointList")]
public class SpawnpointlistSO : ScriptableObject
{
    public List<SpawnPointWorker> list;
}

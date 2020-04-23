using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpawnStuff/Level")]
// [System.Serializable]
public class Level:ScriptableObject
{
    public int lvlID;
    public Area[] areas;
}

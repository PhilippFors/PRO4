using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "new Level", menuName = "Level")]
// [System.Serializable]
public class Level:ScriptableObject
{
    public int lvlID;
    public Area[] areas;

    
}

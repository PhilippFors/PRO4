using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
[System.Serializable]
public class Level:ScriptableObject
{
    public int lvlID;
    public Area[] areas;

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Author(mainAuthor = "Philipp Forstner")]
[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
[System.Serializable]
public class LevelData:ScriptableObject
{
    public int lvlID;
    public Objective[] areas;
}

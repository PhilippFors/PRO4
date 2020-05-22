using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SceneLevelData : MonoBehaviour
{
     public Level levelInfo;
    public void OnEnable()
    {
        if (levelInfo == null)
            levelInfo = ScriptableObject.CreateInstance<Level>();
    }
}

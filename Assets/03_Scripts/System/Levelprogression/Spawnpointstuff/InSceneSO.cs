using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InSceneSO : MonoBehaviour
{
  public Level level;
    public void OnEnable()
    {
        if (level == null)
            level = ScriptableObject.CreateInstance<Level>();
    }
}

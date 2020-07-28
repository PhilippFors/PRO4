using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[ExecuteInEditMode]
public class SpawnpointID : MonoBehaviour
{
    public PlayableDirector director;
    public int LevelID;
    public int AreaID;
    public int UniqueID;

    int oldLevelID;
    // Start is called before the first frame update
    void Start()
    {
        oldLevelID = LevelID;
    }
    public void UpdateID()
    {
        SpawnpointID[] list = FindObjectsOfType<SpawnpointID>();
        if (LevelID != oldLevelID)
        {
            
            foreach (SpawnpointID id in list)
            {
                id.LevelID = this.LevelID;
            }
            oldLevelID = LevelID;
            
        }
        foreach (SpawnpointID id in list)
        {
            id.gameObject.name = "SpawnPNT: " + "Lvl " + id.LevelID + ", ar " + id.AreaID + ", Unq " + id.UniqueID;
        }
        
    }
}

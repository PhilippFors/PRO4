using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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

    public void UpdateLevelID()
    {
        if (LevelID != oldLevelID)
        {
            SpawnpointID[] list = FindObjectsOfType<SpawnpointID>();
            foreach (SpawnpointID id in list)
            {
                id.LevelID = this.LevelID;
            }
            oldLevelID = LevelID;
        }
    }
}

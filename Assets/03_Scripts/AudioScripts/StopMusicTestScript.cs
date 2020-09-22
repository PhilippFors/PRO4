using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMusicTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    public FMODUnity.StudioEventEmitter emitter;
    FMOD.Studio.EventInstance test;

    void Start()
    {
        

}

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        test = FMODAudioPeer._instance.getMusicInstance();
        test.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        Debug.Log("StopMusic");
    }
    
}


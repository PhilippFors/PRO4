using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSkillsController : MonoBehaviour
{

    public GameObject AudioPeer;
    FMODUnity.StudioEventEmitter emitter;

    bool lowPassActive;
    bool highPassActive;
    bool active;

    // Start is called before the first frame update
    void Start()
    {
        lowPassActive = false;
        highPassActive = false;
        emitter = AudioPeer.GetComponent<FMODUnity.StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1") && !highPassActive)
        {
            Debug.Log("Pressed 1");
            if (!lowPassActive)
            {
                
                lowPassActive = true; 
                emitter.SetParameter("LowPass", 0.3f);
            }
            else
            {
                lowPassActive = false;
                emitter.SetParameter("LowPass", 1f);
            }
        }

        if (Input.GetKeyDown("2") && !lowPassActive)
        {
            emitter = AudioPeer.GetComponent<FMODUnity.StudioEventEmitter>();
            if (!highPassActive)
            {
                highPassActive = true;
                emitter.SetParameter("HighPass", 0.5f);
            }
            else
            {
                highPassActive = false;
                emitter.SetParameter("HighPass", 1f);
            }
        }
    }
}

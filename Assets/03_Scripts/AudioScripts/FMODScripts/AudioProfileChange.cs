using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioProfileChange : MonoBehaviour
{
    public float[] values = new float[8];
    bool active = false;
    //FMODAudioPeer m_peer = FMODAudioPeer._instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!active && (other.gameObject.tag.Equals("Player")))
        {
            FMODAudioPeer._instance.changeAudioProfile(values);
        }
       
    }
}

using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SectionKickLaser(int sectionID)
    {
     
    }


    public void parseMarkerString(String marker)
    {
        if (marker[0] != '&')
        {
            for (int i = 0; i < marker.Length; i++)
            {
                switch (marker[i])
                {
                    case 'K':
                        //Debug.Log("Parsed: Kick");
                        if(EventSystem.instance == null)
                        {
                            Debug.Log("EventSystem is empty");
                        }
                        else
                        {
                            EventSystem.instance.OnKick();
                        }                   
                        break;

                    case 'B':
                       // Debug.Log("Parsed: Bass");
                        if (EventSystem.instance == null)
                        {
                            Debug.Log("EventSystem is empty");
                        }
                        else
                        {
                            EventSystem.instance.OnBass();
                        }
                        break;
                }
            }
        }
        else
        {
            switch (marker)
            {
                case "&KL":
                    SectionKickLaser(1);
                    break;
            }
        }
    }



}

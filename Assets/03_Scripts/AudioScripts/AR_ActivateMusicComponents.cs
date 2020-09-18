using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AR_ActivateMusicComponents : MonoBehaviour
{

    
    public bool m_groupActive = false;
    private bool m_helper = false;
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_groupActive && !m_helper)
        {
            m_helper = true;
            activateGroup();
            
        }

        if (!m_groupActive && m_helper)
        {
            m_helper = false;
            deactivateGroup();
        }
        
    }

    public void activateGroup()
    {
        AR_mover mover;
        AR_beaconWall laser;
        foreach (Transform child in obj.transform)
        {
       
            if (child.tag == "MusicObstacle")
            {
                mover = child.GetComponent<AR_mover>();
                if(mover != null)
                {
                    mover.activateComponent();
                }
                laser = child.GetComponent<AR_beaconWall>();
                if (laser != null)
                {
                    laser.activateComponent();
                }

            }
               

        }
    }

    public void deactivateGroup()
    {
        AR_mover mover;
        AR_beaconWall laser;
        foreach (Transform child in obj.transform)
        {

            if (child.tag == "MusicObstacle")
            {
                mover = child.GetComponent<AR_mover>();
                if (mover != null)
                {
                    mover.deactivateComponent();
                }
                laser = child.GetComponent<AR_beaconWall>();
                if (laser != null)
                {
                    laser.deactivateComponent();
                }

            }


        }
    }

}

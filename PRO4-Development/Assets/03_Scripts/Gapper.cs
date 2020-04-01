using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gapper : MonoBehaviour
{
    private bool isGapped;
    //up = -235
    //down = -270

    void Start()
    {
        isGapped = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isGapped == false)
            {
                transform.position = transform.position + new Vector3(0, 0, 84);
                isGapped = true;
            }

            else
            {

                transform.position = transform.position + new Vector3(0, 0, -84);
                isGapped = false;
            }

        }


    }
}

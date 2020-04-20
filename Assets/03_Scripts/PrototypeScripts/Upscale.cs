using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upscale : MonoBehaviour
{
    private bool isUp;
    //up = -235
    //down = -270

    void Start()
    {
        isUp = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (isUp == false){
                transform.position = transform.position + new Vector3(0, 35, 0);
                isUp = true;
            } 

            else{
            
                transform.position = transform.position + new Vector3(0, -35, 0);
                isUp = false;
                }
            
        }


    }
}

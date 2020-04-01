using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downer : MonoBehaviour
{
    private bool isVisible;
    public GameObject wall;
 
    void Start()
    {
        isVisible = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isVisible == true)
            {
                wall.SetActive(false);
                isVisible = false;
            } else {
                wall.SetActive(true);
                isVisible = true;
            }

        }

    }
}

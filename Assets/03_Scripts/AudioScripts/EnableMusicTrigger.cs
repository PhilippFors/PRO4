using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMusicTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public bool subscribeToEvent;
    private bool helper = false;
    public GameObject triggerObj;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (subscribeToEvent && !helper)
        {
            helper = true;
            MyEventSystem.instance.waveDefeated += enableTrigger;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        subscribeToEvent = true;
       
    }

    void enableTrigger()
    {
        triggerObj.SetActive(true);
    }



}

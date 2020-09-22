
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntry : MonoBehaviour
{
    bool sceneLoaded = false;
    AR_beaconWall[] beaconWalls => FindObjectsOfType<AR_beaconWall>();
    MusicManager musicManager => FindObjectOfType<MusicManager>();
    AR_lamp[] lamps => FindObjectsOfType<AR_lamp>();
    AR_ColorMaster colorMaster => FindObjectOfType<AR_ColorMaster>();
    AR_damagePlate[] damagePlates => FindObjectsOfType<AR_damagePlate>();
    AR_Light[] lights => FindObjectsOfType<AR_Light>();
    AR_mover[] movers => FindObjectsOfType<AR_mover>();

    private void OnEnable()
    {
        // SceneManager.sceneLoaded += StartLevel;
        GameManager.instance.initAll += StartLevel;
    }

    private void Awake()
    {
        // if (!sceneLoaded & SceneManager.GetSceneByName("Base").isLoaded)
        // {
        //     sceneLoaded = true;
        //     StartCoroutine(WaitFStart());
        // }
    }
    private void Start()
    {
        if (!sceneLoaded)
        {
            StartLevel();
        }
    }

    void StartLevel()
    {
        sceneLoaded = true;
        StartCoroutine(WaitFStart());
    }

    IEnumerator WaitFStart()
    {
        yield return new WaitForEndOfFrame();
        LevelEventSystem.instance.LevelEntry();
        GameManager.instance.initAll -= StartLevel;

        if (beaconWalls != null)
            foreach (AR_beaconWall w in beaconWalls)
            {
                w.addActionToEvent();
                w.activateComponent();
            }
        if (lamps != null)
            foreach (AR_lamp l in lamps)
            {
                l.addActionToEvent();
                l.activateComponent();
            }

        if (damagePlates != null)
            foreach (AR_damagePlate d in damagePlates)
            {
                d.addActionToEvent();
                d.activateComponent();
            }

        if (lights != null)
            foreach (AR_Light s in lights)
            {
                s.addActionToEvent();
                s.activateComponent();
            }

        if (movers != null)
            foreach (AR_mover s in movers)
            {
                s.addActionToEvent();
                s.activateComponent();
            }
        musicManager.Init();
        colorMaster.activateComponent();
    }
}

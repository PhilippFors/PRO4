using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

[Author("Philipp Forstner")]
public class SpawnDebugging : MonoBehaviour
{
    public AIManager manager;
    public EnemySet set;
    public GameObject Avik;
    public GameObject Shentau;
    public EnemyType enemyType;
    public bool setActive;
    public bool spawnEnable = false;
    private void Update()
    {
        if (setActive)
            SetActive();

        if (spawnEnable)
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CheckEnemy();
            }
    }

    public void CheckEnemy()
    {
        switch (enemyType)
        {
            case EnemyType.Avik:
                Spawn(Avik);
                break;
            case EnemyType.Shentau:
                Spawn(Shentau);
                break;
        }
    }
    public void Spawn(GameObject obj)
    {

        Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, GameObject.FindGameObjectWithTag("Player").transform.position.y, 0));
        //creating the Ray
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        float rayLength;
        //checking if the raycast intersects with the plane
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 rayPoint = cameraRay.GetPoint(rayLength);
            //Debug.DrawLine(cameraRay.origin, rayPoint);
            EnemyBody enemy = Instantiate(obj, rayPoint, Quaternion.Euler(Vector3.forward)).gameObject.GetComponentInChildren<EnemyBody>();
            enemy.GetComponent<StateMachineController>().aiManager = manager;
            enemy.gameObject.GetComponent<Animation>().enabled = false;
            set.Add(enemy);
        }
    }

    public void SetActive()
    {
        foreach (EnemyBody enemy in set.entityList)
            MyEventSystem.instance.ActivateAI(enemy);

    }
}
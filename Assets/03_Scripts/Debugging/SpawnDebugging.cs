using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpawnDebugging : MonoBehaviour
{
    public EnemySet set;
    public GameObject prefab;
    public bool spawnEnable = false;
    public void EnableSpawnclick()
    {
        spawnEnable = !spawnEnable;
    }
    private void Update()
    {
        if (!spawnEnable)
            return;

        Spawn();
    }
    public void Spawn()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
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
                EnemyBody enemy = Instantiate(prefab, rayPoint, Quaternion.Euler(Vector3.forward)).gameObject.GetComponentInChildren<EnemyBody>();
                enemy.gameObject.GetComponent<Animation>().enabled =false;
                enemy.gameObject.GetComponent<NavMeshAgent>().enabled = false;
                set.Add(enemy);
            }
        }
    }
}

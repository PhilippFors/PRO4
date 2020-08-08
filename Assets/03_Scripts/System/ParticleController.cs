using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public ParticleSystem hitParticle;
    public ParticleSystem electricity;
    public Transform[] particlePoints;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyBody>() != null)
        {
            Vector3[] closepoints = new Vector3[particlePoints.Length];
            for(int i = 0; i< particlePoints.Length; i++){
               closepoints[i] = other.ClosestPoint(particlePoints[i].position);
            }
            StartParticles(closepoints);
            Debug.Log("Hit: " + other.gameObject.name);
        }
    }

    void StartParticles(Vector3[] points)
    {
        for (int i = 0; i < particlePoints.Length; i++)
        {
            Instantiate(hitParticle, points[i], new Quaternion(0, 0, 0, 0));
            Instantiate(electricity, points[i], new Quaternion(0, 0, 0, 0));
        }
    }
}

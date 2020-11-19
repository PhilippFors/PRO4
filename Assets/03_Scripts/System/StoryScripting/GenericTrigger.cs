using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
public class GenericTrigger : MonoBehaviour
{
    public TriggerBehaviour behaviour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerBody>())
        {
            behaviour.Execute(other, this);
            if (!behaviour.stayActive)
                gameObject.SetActive(false);
        }
    }
}

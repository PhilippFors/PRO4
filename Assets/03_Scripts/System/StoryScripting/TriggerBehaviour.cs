using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
public abstract class TriggerBehaviour : ScriptableObject
{
    public bool stayActive = false;
    public abstract void Execute(Collider other, GenericTrigger t = null);
}

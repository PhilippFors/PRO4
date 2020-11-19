using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author(mainAuthor = "Philipp Forstner")]
public abstract class Weapon : MonoBehaviour
{
    abstract public void Activate();
    abstract public void Deactivate();
}

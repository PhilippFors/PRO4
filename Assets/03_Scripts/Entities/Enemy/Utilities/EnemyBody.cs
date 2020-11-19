using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Author("Philipp Forstner")]
public class EnemyBody : MonoBehaviour
{
    public Symbol symbolInfo;
    public Transform rayEmitter;
    public BoxCollider hitBox;
    public PlayerDetector playerDetector => hitBox.GetComponent<PlayerDetector>();
    public AIManager aiManager;
    public GameObject symbol;

    void InitSymbol()
    {
        // symbol.GetComponent<MeshRenderer>().material = symbolInfo.mat;
        MeshRenderer[] rend = symbol.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in rend)
            r.material = symbolInfo.mat;
    }
}

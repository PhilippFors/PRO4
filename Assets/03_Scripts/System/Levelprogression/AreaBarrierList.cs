using System.Collections.Generic;
using UnityEngine;

[Author(mainAuthor = "Philipp Forstner")]
[CreateAssetMenu(fileName = "New Barrierlist", menuName = "Barrierlist")]
public class AreaBarrierList : ScriptableObject
{
    public List<AreaBarrier> list;
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class TutorialTemplate : MonoBehaviour
{
    public List<ObjectList> objectList;
    public int currentObjectListID;
    private int[] objectIndex;

    private void OnEnable()
    {
        objectIndex = new int[objectList[currentObjectListID].frontObjects.Count];
        SetSiblingsToFront();
    }

    private void OnDisable()
    {
        transform.SetAsLastSibling();
        SetSiblingsBack();
        
    }

    void SetSiblingsToFront()
    {
        int i = 0;
        transform.SetAsLastSibling();
        foreach (var front in objectList[currentObjectListID].frontObjects)
        {
            objectIndex[i] = (front.GetSiblingIndex());
            front.SetAsLastSibling();
            i++;
        }
    }

    void SetSiblingsBack()
    {
        int i = 0;
        foreach (var front in objectList[currentObjectListID].frontObjects)
        {
            front.SetSiblingIndex(objectIndex[i]);
            i++;
        }
    }

    
    [System.Serializable]
    public class ObjectList
    {
        public List<Transform> frontObjects;
    }


}
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TutorialTemplate : MonoBehaviour
{
    public List<Transform> frontObjects;
    private int[] objectIndex;

    private void OnEnable()
    {
        objectIndex = new int[frontObjects.Count];
        SetSiblingsToFront();
    }

    private void OnDisable()
    {
        SetSiblingsBack();
        transform.SetAsLastSibling();
    }

    void SetSiblingsToFront()
    {
        int i = 0;
        transform.SetAsLastSibling();
        foreach (var front in frontObjects)
        {
            objectIndex[i] = (front.GetSiblingIndex());
            front.SetAsLastSibling();
            i++;
        }
    }

    void SetSiblingsBack()
    {
        int i = 0;
        foreach (var front in frontObjects)
        {
            front.SetSiblingIndex(objectIndex[i]);
            i++;
        }
    }



}
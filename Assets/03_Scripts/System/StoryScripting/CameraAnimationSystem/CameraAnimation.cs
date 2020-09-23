using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    private void Start()
    {
        MyEventSystem.instance.startCamAnim += StartAnimation;
    }

    public void StartAnimation(Transform camera, Transform endposition, Transform player, Transform playerDest)
    {
        StartCoroutine(Animate(camera, endposition, player, playerDest));
    }

    IEnumerator Animate(Transform camera, Transform endPosition, Transform player, Transform playerDest)
    {
        while (camera.position != endPosition.position)
        {
            yield return null;

            camera.position = Vector3.Lerp(camera.position, endPosition.position, 0.03f * Time.deltaTime);
        }

    }
}

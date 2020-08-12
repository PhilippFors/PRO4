using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 playerPosition;
    public Camera mainCam;
    private PlayerStateMachine playercontrols;
    public float cameraFollowSpeed = 5f;
    public float targetBias = 0.5f;
    Plane groundPlane;
    private void Start()
    {
       playercontrols = player.gameObject.GetComponent<PlayerStateMachine>();
    }
    private void Update()
    {
        Vector3 s = getCamPosition();
        s.y = player.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, s, Time.deltaTime * cameraFollowSpeed);
    }

    Vector3 getCamPosition()
    {
        groundPlane = new Plane(Vector3.up, new Vector3(0, player.position.y, 0));
        //creating the Ray
        Ray cameraRay = mainCam.ScreenPointToRay(playercontrols.mouseLook);

        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 rayPoint = cameraRay.GetPoint(rayLength);

            Vector3 dist = rayPoint - player.position;
            return player.position + dist * targetBias;
        }

        return player.position;
    }

}

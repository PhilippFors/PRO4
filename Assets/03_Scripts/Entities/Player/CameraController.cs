using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player => GameObject.FindGameObjectWithTag("Player");
    private Vector3 playerPosition;
    public Camera mainCam;
    private PlayerStateMachine playercontrols;
    public float cameraFollowSpeed = 5f;
    float width = Screen.width;
    float height = Screen.height;
    float oldX = 0;
    float oldY = 0;

    public float targetBias = 0.5f;
    private void Start()
    {
        playercontrols = player.GetComponent<PlayerStateMachine>();
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }
    private void Update()
    {
        Vector3 s = getDifference();
        s.y = player.transform.position.y;
        transform.position = Vector3.Lerp(transform.position, s, Time.deltaTime * cameraFollowSpeed);
    }

    Vector3 getDifference()
    {
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, player.transform.position.y, 0));
        //creating the Ray
        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);

        float rayLength;

        //checking if the raycast intersects with the plane
        oldX = Input.mousePosition.x;
        oldY = Input.mousePosition.y;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 rayPoint = cameraRay.GetPoint(rayLength);

            Vector3 dist = rayPoint - player.transform.position;
            return player.transform.position + dist * targetBias;
        }

        return player.transform.position;
    }

}

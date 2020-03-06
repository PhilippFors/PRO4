using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera mainCam;
    Vector3 forward, right;
    Vector3 moveVelocity;
    Vector3 pointToLook;
    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    private void Update()
    {

        if (Input.anyKey)
            Move();

        Look();

    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("HorizontalKey"), 0, Input.GetAxisRaw("VerticalKey"));
        if ((Input.GetAxisRaw("HorizontalKey") == 1 || Input.GetAxisRaw("HorizontalKey") == -1) & Input.GetAxisRaw("VerticalKey") == 0)
        {
            moveVelocity = direction * (moveSpeed + 2); ;
        }
        else if ((Input.GetAxisRaw("VerticalKey") == 1 || Input.GetAxisRaw("VerticalKey") == -1) & Input.GetAxisRaw("HorizontalKey") == 0)
        {
            moveVelocity = direction * (moveSpeed + 2);
        }
        else
        {
            moveVelocity = direction * moveSpeed;
        }


        Vector3 horizMovement = right * moveVelocity.x * Time.deltaTime;
        Vector3 vertiMovment = forward * moveVelocity.z * Time.deltaTime;

        transform.position += horizMovement;
        transform.position += vertiMovment;
    }

    void Look()
    {
        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);

        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }


}

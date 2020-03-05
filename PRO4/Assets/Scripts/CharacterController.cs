using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Camera mainCam;
    Vector3 forward, right;
    Vector3 moveInput;
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
        moveInput = new Vector3(Input.GetAxisRaw("HorizontalKey"), 0, Input.GetAxisRaw("VerticalKey"));
        moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);
        
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("HorizontalKey"), 0, Input.GetAxisRaw("VerticalKey"));
        Vector3 horizMovement = right * moveVelocity.x * Time.deltaTime;
        Vector3 vertiMovment = forward * moveVelocity.z * Time.deltaTime;

        Vector3 heading = Vector3.Normalize(pointToLook);
        
        
        transform.position += horizMovement;
        transform.position += vertiMovment;

    }


}

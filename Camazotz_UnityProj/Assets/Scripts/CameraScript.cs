using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Vector3 yOffset; //offset of ground, Augenhöhe
    public Vector3 defaultPosition; //default position of cam

    public LayerMask environmentLayer; //walls and stuff

    // ---
    public float wallStopDistance;
    public Camera actualCamera;

    public GameObject player;

    // ---
    float cameraRotationSpeed = 75f;
    public float minRotationX;
    public float maxRotationX;

    public bool IsCameraBehindWall(out RaycastHit hit)
    {
        return Physics.Linecast(transform.position + yOffset, transform.position + transform.rotation * defaultPosition, out hit, environmentLayer);
    }

    private void Start()
    {
        actualCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        //Cam mover
        transform.Rotate(Vector3.up, -Input.GetAxis("Horizontal_right") * cameraRotationSpeed * Time.fixedDeltaTime, Space.World);

        Quaternion q = transform.rotation;
        float currentRotationX = Mathf.Asin(2 * (q.w * q.x - q.z * q.y));

        // Limit the camera rotation on the X axis
        if ((currentRotationX > minRotationX && Input.GetAxis("Vertical_right") < 0) || (currentRotationX < maxRotationX && Input.GetAxis("Vertical_right") > 0))
            transform.Rotate(Vector3.right, Input.GetAxis("Vertical_right") * cameraRotationSpeed * Time.fixedDeltaTime, Space.Self);

        // Prevent wall clipping
        RaycastHit hit;
        if (IsCameraBehindWall(out hit))
        {
            // Offset the camera position slightly from the wall
            actualCamera.transform.position = Vector3.Lerp(transform.position + yOffset, hit.point + hit.normal * wallStopDistance, 1 - wallStopDistance);
        }
        else
        {
            actualCamera.transform.localPosition = Vector3.Lerp(yOffset, defaultPosition, 1 - wallStopDistance);
        }

        transform.position = player.transform.position;
    }
}

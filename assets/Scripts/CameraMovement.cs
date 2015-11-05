// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
    //
    // VARIABLES
    //

    public float turnSpeed = 4.0f;		// Speed of camera turning when mouse moves in along an axis
    public float panSpeed = 4.0f;		// Speed of the camera when being panned
    public float zoomSpeed = 4.0f;		// Speed of the camera going back and forth
    public float maxYRotation;
    public float minYRotation;
    private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
    private bool isPanning;		// Is the camera being panned?
    private bool isRotating;	// Is the camera being rotated?
    private bool isZooming;		// Is the camera zooming?

    void Start()
    {
        Debug.Log(transform.eulerAngles);
    }

    //
    // UPDATE
    //

    void Update()
    {
        // Get the left mouse button
        if (Input.GetMouseButtonDown(1))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isRotating = true;
        }

        // Get the right mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }

        // Get the middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            // Get mouse origin
            mouseOrigin = Input.mousePosition;
            isZooming = true;
        }

        // Disable movements on button release
        if (!Input.GetMouseButton(1)) isRotating = false;
        if (!Input.GetMouseButton(0)) isPanning = false;
        if (!Input.GetMouseButton(2)) isZooming = false;

        // Rotate camera along X and Y axis
        if (isRotating)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            transform.RotateAround(transform.position, transform.right, -pos.y * turnSpeed);
            transform.RotateAround(transform.position, Vector3.up, pos.x * turnSpeed);
            Debug.Log(transform.eulerAngles);
            if (transform.eulerAngles.x > maxYRotation) transform.eulerAngles = new Vector3(maxYRotation, transform.eulerAngles.y, transform.eulerAngles.z);
            if (transform.eulerAngles.x < minYRotation) transform.eulerAngles = new Vector3(minYRotation, transform.eulerAngles.y, transform.eulerAngles.z);
        }

        // Move the camera on it's XY plane
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0)*-1;
            transform.Translate(move, Space.Self);
        }

        // Move the camera linearly along Z axis
        //if (isZooming)
        //{
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(0, 0, scroll * zoomSpeed, Space.Self);
            //Vector3 posi = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            //Vector3 movei = posi.y * zoomSpeed * transform.forward * scroll;
            //transform.Translate(movei, Space.World);
        //}
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class char_cameraFollow : MonoBehaviour
{
    [SerializeField] private float followSpeed = 0.1f;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Camera char_cam;
    private float zoom;
    private float zoomMult = 50f;
    private float min_Zoom = 5f;
    private float max_Zoom = 15f;
    private float velocity = 0f;
    private float smooth = 0.25f;


    // Start is called before the first frame update
    void Start()
    {
        zoom = char_cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        // Calls the instance of the character to be followed 
        transform.position = Vector3.Lerp(transform.position, PlayerController.Instance.transform.position + offset, followSpeed);

        // Control for the ScrollWheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // Calculates the speed of the scroll and how much it'll zoom in or out
        zoom -= scroll * zoomMult;
        zoom = Mathf.Clamp(zoom, min_Zoom, max_Zoom);

        // Controls how the camera is following the character
        char_cam.orthographicSize = Mathf.SmoothDamp(char_cam.orthographicSize, zoom, ref velocity, smooth);

    }
}

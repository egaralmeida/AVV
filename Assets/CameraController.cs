using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public Transform followPoint;
    public Transform planet;
    public Transform player;

    public float dampTime = 0.15f;
    public float zoomLevel = 5;
    public float minZoomOffset = 0;
    public float maxZoomOffset = 2;

    private Vector3 velocity = Vector3.zero;
    private Camera myCamera;

    // It's like the big bang
    void Start()
    {
        myCamera = this.GetComponent<Camera>();
        zoomLevel = myCamera.orthographicSize;
    }

    // It's like evolution
    void FixedUpdate()
    {
        myCamera.orthographicSize = Mathf.Clamp(Vector2.Distance(followPoint.position, planet.position).map(1.5f, 3.5f, minZoomOffset, maxZoomOffset), minZoomOffset, maxZoomOffset);
        zoomLevel = myCamera.orthographicSize;
        Vector3 point = myCamera.WorldToViewportPoint(followPoint.position);
        Vector3 delta = followPoint.position - myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
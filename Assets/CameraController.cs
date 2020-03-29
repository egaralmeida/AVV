using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform followPoint;
    public Transform planet;
    public Transform player;

    public float dampTime = 0.15f;
    public float minZoomOffset = 2.5f;
    public float maxZoomOffset = 4f;

    private Vector3 velocity = Vector3.zero;
    private Camera myCamera;
    private float minDistance;
    private float maxDistance;

    // It's like the big bang
    void Start()
    {
        myCamera = this.GetComponent<Camera>();

        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            minDistance = playerScript.semiMinor;
            maxDistance = playerScript.semiMajor;
        }
    }

    // It's like evolution
    void FixedUpdate()
    {
        float distPlanet = Vector2.Distance(player.position, planet.position);

        myCamera.orthographicSize = Mathf.Clamp(distPlanet.map(minDistance, maxDistance, minZoomOffset, maxZoomOffset), minZoomOffset, maxZoomOffset);

        Vector3 point = myCamera.WorldToViewportPoint(followPoint.position);
        Vector3 delta = followPoint.position - myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }
}
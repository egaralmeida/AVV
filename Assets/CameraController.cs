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

    private Vector3 _velocity = Vector3.zero;
    private Camera _myCamera;
    private float _minDistance;
    private float _maxDistance;

    // It's like the big bang
    void Start()
    {
        _myCamera = this.GetComponent<Camera>();

        Player playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            _minDistance = playerScript.semiMinor;
            _maxDistance = playerScript.semiMajor;
        }
    }

    // It's like evolution
    void FixedUpdate()
    {
        float distPlanet = Vector2.Distance(player.position, planet.position);

        _myCamera.orthographicSize = Mathf.Clamp(distPlanet.map(_minDistance, _maxDistance, minZoomOffset, maxZoomOffset), minZoomOffset, maxZoomOffset);

        Vector3 point = _myCamera.WorldToViewportPoint(followPoint.position);
        Vector3 delta = followPoint.position - _myCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;

        transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, dampTime);
    }
}
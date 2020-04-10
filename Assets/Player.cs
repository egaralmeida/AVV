using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // References
    public Transform planet;
    public Transform followPoint;
    public GameObject projectile;
    public Transform shootingPoint;

    // Config Variables    
    public float semiMajor = 7f; //TODO: move these to the planet's ScriptableObject?
    public float semiMinor = 4f; ///
    [SerializeField] private float _movementSpeed = 90;
    [SerializeField] private float _orbitPosition = -180f;
    [SerializeField] private float _movement = 0f;

    // Follow Point
    public float minOffsetY = 2f; // TODO: make private
    public float maxOffsetY = 4.75f;

    // Since these comments are auto added when creating the file, I'm gonna replace them for freedom.
    void Start()
    {

    }

    // Like here, for example: This method updates. You're welcome.
    void Update()
    {
        _movement = Input.GetAxisRaw("Horizontal");

        this.transform.lookAt2D(planet.transform.position);

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        UpdateFollowPoint();

    }

    void FixedUpdate()
    {
        transform.position = new Vector2(planet.position.x + (semiMajor * Mathf.Sin(Mathf.Deg2Rad * _orbitPosition)),
                                         planet.position.y + (semiMinor * Mathf.Cos(Mathf.Deg2Rad * _orbitPosition)));

        _orbitPosition -= _movement * Time.fixedDeltaTime * _movementSpeed;
    }

    void Fire()
    {
        GameObject _myShot = Instantiate(projectile, shootingPoint.position, this.transform.rotation);
        Shot myShotScript = (Shot)_myShot.GetComponent(typeof(Shot));
        myShotScript.parent = this.transform;
    }

    void UpdateFollowPoint()
    {
        Vector3 localPos = followPoint.localPosition;
        float distPlayerPlanet = Vector2.Distance(this.transform.position, planet.position);
        localPos.x = Mathf.Clamp(distPlayerPlanet.map(semiMinor, semiMajor, minOffsetY, maxOffsetY), minOffsetY, maxOffsetY);
        followPoint.localPosition = localPos;
    }
}

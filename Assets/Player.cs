using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // References
    public GameObject planet;
    public GameObject followPoint;
    public GameObject projectile;
    private GameObject _myShot;
    private Transform _fPointTrans;

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
        _fPointTrans = followPoint.transform;//this.GetComponentInChildren<Transform>();
        if (_fPointTrans == null)
            Debug.Log("Can't get followPoint's Transform");
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
        transform.position = new Vector2(planet.transform.position.x + (semiMajor * Mathf.Sin(Mathf.Deg2Rad * _orbitPosition)),
                                         planet.transform.position.y + (semiMinor * Mathf.Cos(Mathf.Deg2Rad * _orbitPosition)));

        _orbitPosition -= _movement * Time.fixedDeltaTime * _movementSpeed;
    }

    void Fire()
    {
        _myShot = Instantiate(projectile, this.transform.position, this.transform.rotation);
        Shot myShotScript = (Shot)_myShot.GetComponent(typeof(Shot));
        myShotScript.parent = this.transform;
    }

    void UpdateFollowPoint()
    {
        Vector3 localPos = _fPointTrans.localPosition;
        float distPlayerPlanet = Vector2.Distance(this.transform.position, planet.transform.position);
        localPos.y = Mathf.Clamp(distPlayerPlanet.map(semiMinor, semiMajor, minOffsetY, maxOffsetY), minOffsetY, maxOffsetY);
        _fPointTrans.localPosition = localPos;
    }
}

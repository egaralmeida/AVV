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
    private Renderer myRenderer;

    // Config Variables    
    public float semiMajor = 7f; // TODO: move these to the GameManager's Scriptable Object
    public float semiMinor = 4f; //
    [SerializeField] private float _movementSpeed = 90;
    [SerializeField] private float _orbitPosition = -180f;
    [SerializeField] private float _movement = 0f;
    [SerializeField] private float _fireRateDelay = 0.1f;

    // Follow Point
    private float _minOffsetY = 2f; // TODO: make private
    private float _maxOffsetY = 4.75f;

    public float MinOffsetY { get => _minOffsetY; set => _minOffsetY = value; }
    public float MaxOffsetY { get => _maxOffsetY; set => _maxOffsetY = value; }

    // Work variables
    private float _fireTimer = 0f;
    private bool _firstClickShotDone = false;

    // Since these comments are auto added when creating the file, I'm gonna replace them for freedom.
    void Start()
    {
        myRenderer = this.GetComponent<Renderer>();
    }

    // Like here, for example: This method updates. You're welcome.
    void Update()
    {

        myRenderer.material.SetFloat("_AlphaIntensity_Fade_2", Random.Range(2.5f, 3f));

        _movement = Input.GetAxisRaw("Horizontal");

        this.transform.lookAt2D(planet.transform.position);

        if (Input.GetButton("Fire1"))
        {
            if (!_firstClickShotDone)
            {
                Fire();
                _firstClickShotDone = true;
            }

            _fireTimer += Time.deltaTime;
            if (_fireTimer >= _fireRateDelay)
            {
                Fire();
                _fireTimer = 0;
            }
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            _firstClickShotDone = false;
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
        Projectile myShotScript = (Projectile)_myShot.GetComponent(typeof(Projectile));
        myShotScript.parent = this.transform;
    }

    void UpdateFollowPoint()
    {
        Vector3 localPos = followPoint.localPosition;
        float distPlayerPlanet = Vector2.Distance(this.transform.position, planet.position);
        localPos.x = Mathf.Clamp(distPlayerPlanet.map(semiMinor, semiMajor, MinOffsetY, MaxOffsetY), MinOffsetY, MaxOffsetY);
        followPoint.localPosition = localPos;
    }
}

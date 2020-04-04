using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // References
    public GameObject planet;
    public GameObject followPoint;
    public GameObject projectile;
    private GameObject myShot;
    private Transform fPointTrans;

    // Config Variables    
    public float movementSpeed = 1.5f;
    public float semiMajor = 7f;
    public float semiMinor = 4f;
    public float orbitPosition = -180f;
    private float movement = 0f;

    // Follow Point
    public float minOffsetY = 2f;
    public float maxOffsetY = 4.75f;

    // Since these comments are auto added when creating the file, I'm gonna replace them for freedom.
    void Start()
    {
        fPointTrans = followPoint.transform;//this.GetComponentInChildren<Transform>();
        if (fPointTrans == null)
            Debug.Log("Can't get followPoint's Transform");
    }

    // Like here, for example: This method updates. You're welcome.
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");

        this.transform.lookAt2D(planet.transform.position);

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }

        UpdateFollowPoint();

    }

    void FixedUpdate()
    {
        transform.position = new Vector2(planet.transform.position.x + (semiMajor * Mathf.Sin(Mathf.Deg2Rad * orbitPosition)),
                                         planet.transform.position.y + (semiMinor * Mathf.Cos(Mathf.Deg2Rad * orbitPosition)));

        orbitPosition -= movement * Time.fixedDeltaTime * movementSpeed;
    }

    void Fire()
    {
        myShot = Instantiate(projectile, this.transform.position, this.transform.rotation);
        Shot myShotScript = (Shot)myShot.GetComponent(typeof(Shot));
        myShotScript.parent = this.transform;
    }

    void UpdateFollowPoint()
    {
        Vector3 localPos = fPointTrans.localPosition;
        float distPlayerPlanet = Vector2.Distance(this.transform.position, planet.transform.position);
        localPos.y = Mathf.Clamp(distPlayerPlanet.map(semiMinor, semiMajor, minOffsetY, maxOffsetY), minOffsetY, maxOffsetY);
        fPointTrans.localPosition = localPos;
    }
}

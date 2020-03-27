using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Objects
    public GameObject target;
    public GameObject projectile;
    private GameObject myShot;

    // Control Variables    
    public float movementSpeed = 1.5f;
    public float semiMajor = 5f;
    public float semiMinor = 3f;
    public float orbitPosition = -180f;

    private float movement = 0f;
    private short spriteAngleCorrection = 270; // TODO: Take this to zero when integrating final art.

    // Since these comments are auto added when creating the file, I'm gonna replace them for freedom.
    void Start()
    {

    }

    // Like here, for example: This method updates. You're welcome.
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");

        // LookAt but for 2D | Thanks robertbu http://answers.unity.com/answers/641238/view.html
        Vector3 dir = target.transform.position - this.transform.position;
        float spriteAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + spriteAngleCorrection;
        this.transform.rotation = Quaternion.AngleAxis(spriteAngle, Vector3.forward);

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        transform.position = new Vector2(target.transform.position.x + (semiMajor * Mathf.Sin(Mathf.Deg2Rad * orbitPosition)),
                                         target.transform.position.y + (semiMinor * Mathf.Cos(Mathf.Deg2Rad * orbitPosition)));

        orbitPosition -= movement * Time.fixedDeltaTime * movementSpeed;
    }

    void Fire()
    {
        myShot = Instantiate(projectile, this.transform.position, this.transform.rotation);
        //myShot = myShot.GetComponent<GameObject>();
        Shot myShotScript = (Shot)myShot.GetComponent(typeof(Shot));
        myShotScript.parent = this.transform;
    }
}

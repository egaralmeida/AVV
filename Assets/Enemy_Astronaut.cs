using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Astronaut : Character
{
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float speed = 2f;

    private Rigidbody2D myRigidBody;

    private bool moving = true;
    private Quaternion targetRotation;

    // This is !the_end, my only friend !the_end
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();

        //Random rotation at spawn
        Vector3 initialRotation = new Vector3(0, 0, Random.Range(0, 360));
        this.transform.rotation = Quaternion.Euler(initialRotation);
    }

    // A new version has been released. No release notes, that's for chumps.
    void Update()
    {
        if (moving)
        {
            // Get rotation vector towards player
            Vector3 dir = player.transform.position - this.transform.position;
            dir.Normalize();
            float spriteAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.Euler(0, 0, spriteAngle);

            // Apply the rotation by steps
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {

        }
    }

    void FixedUpdate()
    {
        if (moving)
        {
            Vector3 velocity = new Vector3(speed, 0, 0);
            myRigidBody.AddForce(this.transform.rotation * velocity);
        }

        // Cast a ray looking for the player
        Vector3 dir = player.transform.position - this.transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(this.transform.position,
                                                    dir, Vector2.Distance(player.transform.position,
                                                    this.transform.position),
                                                    1 << 9);

        Debug.DrawRay(this.transform.position, dir);

        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.tag == "Planet")
                moving = false;
            else if (hitInfo.transform.tag == "Player")
                moving = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Astronaut : Character
{
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float fireDistance = 4f;
    public Transform target;

    private Rigidbody2D myRigidBody;
    private Quaternion newRotation;

    private bool moving = true;
    private bool firing = false;

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
            // Get rotation vector towards target
            Vector3 dir = target.transform.position - this.transform.position;
            dir.Normalize();
            float spriteAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            newRotation = Quaternion.Euler(0, 0, spriteAngle);

            // Apply the rotation by steps
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
        }

        if (health <= 0)
        {
            kill();
        }
    }

    void FixedUpdate()
    {
        if (moving)
        {
            Vector3 velocity = new Vector3(speed, 0, 0);
            myRigidBody.AddForce(this.transform.rotation * velocity);
        }

        // Get distance to target
        float distanceToPlayer = Vector2.Distance(target.transform.position, this.transform.position);
        
        // Cast a ray looking for the target
        Vector3 dir = target.transform.position - this.transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(this.transform.position, dir, distanceToPlayer, 1 << 9);

        Debug.DrawRay(this.transform.position, dir);

        // If we don't see the target, 
        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.tag == "Planet")
            {
                moving = false;
            }
            else if (hitInfo.transform.tag == "Player")
            {
                if (distanceToPlayer > fireDistance)
                {
                    moving = true;
                }
                else
                {
                    moving = false;
                    firing = true;
                }
            }
        }
    }
}

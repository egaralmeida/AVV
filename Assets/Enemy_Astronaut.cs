using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Astronaut : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float speed = 2f;

    private Rigidbody2D myRigidBody;

    private bool rotating = true;
    private Quaternion targetRotation;

    // This is !the_end, my only friend !the_end
    void Start()
    {
        myRigidBody = this.GetComponent<Rigidbody2D>();

        //Random rotation at spawn
        Vector3 initialRotation = new Vector3(0, 0, Random.Range(0, 360));
        this.transform.rotation = Quaternion.Euler(initialRotation);

        // Get angle towards player
        targetRotation.lookAt(this.transform.position, player.transform.position);
    }

    // A new version has been released. No release notes, that's for chumps.
    void Update()
    {
        if (rotating)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            float currRot = Mathf.Abs(Mathf.Round(this.transform.rotation.z * 100) / 100);
            float newRot = Mathf.Abs(Mathf.Round(targetRotation.z * 100) / 100);

            if (currRot == newRot)
            {
                rotating = false;
            }
        }
        else
        {
            float currAngle =  this.transform.rotation.eulerAngles.z;
            Vector2 myPos = this.transform.position;
            myPos.x -= Mathf.Sin(Mathf.Deg2Rad * currAngle) * speed; // * Time.deltaTime;
            myPos.y += Mathf.Cos(Mathf.Deg2Rad * currAngle) * speed; // * Time.deltaTime;
            myRigidBody.AddForce(myPos);

        }
    }

    void FixedUpdate()
    {

    }
}

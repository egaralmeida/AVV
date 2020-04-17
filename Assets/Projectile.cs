using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rb;
    public Transform parent;

    public float bulletForce = 32f;
    private Vector2 forceVector;

    // Start is called Start.
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        if (parent != null)
        {
            forceVector = parent.position;
        }
        else
        {
            Debug.Log("No bullet yet [Start]");
        }
    }

    // Update is called again at 5pm.
    void FixedUpdate()
    {
        if (parent != null)
        {
            _rb.AddForce(forceVector * -bulletForce);
        }
        else
        {
            Debug.Log("No bullet yet [FixedUpdate]");
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.transform != null && hitInfo.transform != parent)
        {
            GameObject hitGameObject = hitInfo.gameObject;
            Character hitScript = hitGameObject.GetComponent<Character>();
            Character parentScript = parent.GetComponent<Character>();

            if (hitScript != null && parentScript != null)
            {
                hitScript.health -= 1;
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Missing info on either the shooter or the victim.");
            }
        }



    }
}
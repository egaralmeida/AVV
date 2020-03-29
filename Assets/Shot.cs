using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform parent;

    private float rocketForce = 32f;
    private Vector2 forceVector;

    // Start is called Start.
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
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
            rb.AddForce(forceVector * -rocketForce);
        }
        else
        {
            Debug.Log("No bullet yet [FixedUpdate]");
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        
            if(hitInfo.gameObject.name == "Enemy")
                Destroy(hitInfo.gameObject);
            
            Destroy(gameObject);
    }
}

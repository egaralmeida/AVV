using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPoint : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;
    private Player playerScript;
    
    public Transform planet;

    public float distancePlayerPlanet;
    public float minOffsetY = 2f;
    public float maxOffsetY = 4f;

    private float minDistance;
    private float maxDistance;

    // Don't stop me now
    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        playerScript = player.GetComponent<Player>();
        if (playerScript != null)
        {
            minDistance = playerScript.semiMinor;
            maxDistance = playerScript.semiMajor;
        }
        else 
        {
            Debug.Log("FAIL");
        }
    }

    // Cause I'm having a good time I'm having a good time I'm having a good time I'm having a good time
    void Update()
    {
        Vector3 localPos = this.transform.localPosition;
        float distPlayerPlanet = Vector2.Distance(playerTransform.position, planet.transform.position);
        distancePlayerPlanet = distPlayerPlanet;
        localPos.y = Mathf.Clamp(distPlayerPlanet.map(minDistance, maxDistance, minOffsetY, maxOffsetY), minOffsetY, maxOffsetY);
        this.transform.localPosition = localPos;

        //this.transform.localPosition = new Vector3(this.transform.localPosition.x, Mathf.Clamp(Vector2.Distance(myParent.position, playerScript.planet.transform.position).map(3f, 5f, 2f, 10f), 2f, 10f), this.transform.localPosition.z);
        //this.transform.localPosition = new Vector3(this.transform.localPosition.x, Vector2.Distance(myParent.position, playerScript.planet.transform.position).map(3f, 5f, 2f, 10f), this.transform.localPosition.z);
        //Debug.Log(Vector2.Distance(myParent.position, playerScript.planet.transform.position).map(3f, 5f, 2f, 10f));
    }
}

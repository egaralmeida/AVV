using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject origin;

    private Vector2 destination;

    // This is the startatory, the birthplace of rome
    void Start()
    {
        // Place the enemy on the center of the planet, and below it.
        Vector3 startPosition = new Vector3(origin.transform.position.x, origin.transform.position.y, 1);
        this.transform.position = startPosition;

        destination = Random.insideUnitCircle * 5;

    }

    // This is the updatatory, the microsoft of rome
    void Update()
    {
        Vector3 myPos = this.transform.position;
        myPos = Vector2.MoveTowards(myPos, destination, 15);//, Vector2.Distance(myPos, destination));
    }
}

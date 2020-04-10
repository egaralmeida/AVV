using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public SO_Enemy myData;
    public Transform origin;
    public float speed = 1.1f;

    private Vector3 _destination;

    // This is the startatory, the birthplace of rome
    void Start()
    {
        this._health = myData.health;
        this._damage = myData.damage;

        this.transform.position = origin.transform.position;
        this.transform.rotation = origin.transform.rotation;

        Vector3 currRotation = this.transform.rotation.eulerAngles;
        currRotation.z += Mathf.Floor(Random.Range(-3f, 3f));
        this.transform.rotation = Quaternion.Euler(currRotation);
    }

    // This is the updatatory, the microsoft of rome
    void Update()
    {
        Vector3 velocity = new Vector3(speed * Time.deltaTime, 0, 0);
        this.transform.position += this.transform.rotation * velocity;
    }

    float constrainAngle(float angle)
    {
        angle = angle % 360;

        if (angle < 0)
            angle += 360;

        return angle;
    }
}

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
        float angle = Mathf.Deg2Rad * this.transform.rotation.eulerAngles.z;
        Vector2 myPos = this.transform.position;
        myPos.x -= Mathf.Sin(angle) * speed * Time.deltaTime;
        myPos.y += Mathf.Cos(angle) * speed * Time.deltaTime;
        this.transform.position = myPos;
    }

    float constrainAngle(float angle)
    {
        angle = angle % 360;

        if (angle < 0)
            angle += 360;

        return angle;
    }
}

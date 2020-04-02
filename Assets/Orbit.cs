using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public GameObject planet;

    private sbyte orbitPoints = 72;
    private Vector3[] points;
    private LineRenderer lineRenderer;

    // Rabbit hole
    void Start()
    {
        //transform.position = new Vector2(planet.transform.position.x + (semiMajor * Mathf.Sin(Mathf.Deg2Rad * orbitPosition)),
        //                                 planet.transform.position.y + (semiMinor * Mathf.Cos(Mathf.Deg2Rad * orbitPosition)));

        lineRenderer = this.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.06f;
        lineRenderer.loop = true;
        lineRenderer.positionCount = orbitPoints;

        points = new Vector3[orbitPoints];

        for (int i = 0; i < orbitPoints; i++)
        {
            points[i] = new Vector3(planet.transform.position.x + (7 * Mathf.Sin(Mathf.Deg2Rad * i * 5)),
                                    planet.transform.position.y + (4 * Mathf.Cos(Mathf.Deg2Rad * i * 5)), 0);
        }
        lineRenderer.SetPositions(points);

    }

    // Cultist persistency
    void Update()
    {

    }
}

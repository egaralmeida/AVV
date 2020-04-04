using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    private sbyte orbitPoints = 72;
    private Vector3[] points;

    public float shieldRotationTime = 3f;
    public float enemySpawnTime = 1.5f;
    public float shieldSpeed = 1f;
    public Transform shieldTransform;

    private float shieldRotationTimer = 0f;
    private bool shieldRotating = false;
    private Vector3 shieldNewRotation;
    private float enemySpawnTimer = 0f;

    // Rabbit hole
    void Start()
    {
        drawOrbit();

        // shieldTransform = this.GetComponentInChildren<Transform>(); // I keep having issues with this.
        if (shieldTransform == null)
            Debug.Log("Can't find shield transform.");
    }

    // Cultist persistency
    void Update()
    {
        /*
        Every n seconds
            Get random angle
            Rotate towards it

        Every nn seconds
            If not rotating
                deploy enemy
        */

        // Wait until shield can rotate
        shieldRotationTimer += Time.deltaTime;
        if (shieldRotationTimer > shieldRotationTime && !shieldRotating)
        {
            shieldNewRotation = new Vector3(0f, 0f, Random.Range(0f, 360f));
            shieldRotating = true;
            //shieldRotationTimer -= shieldRotationTime;
        }

        if (shieldRotating)
        {
            shieldTransform.rotation = Quaternion.Lerp(shieldTransform.rotation, Quaternion.Euler(shieldNewRotation), Time.deltaTime * shieldSpeed);
            
            float shieldCurrRot = Mathf.Abs(Mathf.Round(shieldTransform.rotation.z * 100) / 100);
            float shieldNewRot = Mathf.Abs(Mathf.Round(Quaternion.Euler(shieldNewRotation).z * 100) / 100);

            if(shieldCurrRot == shieldNewRot)
            {
                shieldRotating = false;
                shieldRotationTimer = 0;
            }
        }

    }

    void drawOrbit()
    {
        LineRenderer lineRenderer = this.gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.06f;
        lineRenderer.loop = true;
        lineRenderer.positionCount = orbitPoints;

        points = new Vector3[orbitPoints];

        for (int i = 0; i < orbitPoints; i++)
        {
            points[i] = new Vector3(this.transform.position.x + (7 * Mathf.Sin(Mathf.Deg2Rad * i * 360 / orbitPoints)),
                                    this.transform.position.y + (4 * Mathf.Cos(Mathf.Deg2Rad * i * 360 / orbitPoints)), 0);
        }
        lineRenderer.SetPositions(points);

    }
}

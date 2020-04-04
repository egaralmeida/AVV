using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public Transform shieldTransform;
    public float shieldRotationTime = 3f;
    public float shieldSpeed = 1f;
    public GameObject currentEnemy;
    public float enemySpawnTime = 1.5f;
    public short enemiesPerDeploy = 3;

    // Orbit work variables
    private sbyte orbitPoints = 72;
    private Vector3[] points;

    // Shield work variables
    private float shieldRotationTimer = 0f;
    private bool shieldRotating = false;
    private Vector3 shieldNewRotation;

    // Enemyspawner work variables
    private float enemySpawnTimer = 0f;
    private bool enemyDeploying = false;
    private short enemiesDeployed = 0;

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
        if (shieldRotating)
        {
            shieldTransform.rotation = Quaternion.Lerp(shieldTransform.rotation, Quaternion.Euler(shieldNewRotation), Time.deltaTime * shieldSpeed);

            float shieldCurrRot = Mathf.Abs(Mathf.Round(shieldTransform.rotation.z * 100) / 100);
            float shieldNewRot = Mathf.Abs(Mathf.Round(Quaternion.Euler(shieldNewRotation).z * 100) / 100);
  
            if (shieldCurrRot == shieldNewRot)
            {
                shieldRotating = false;
                enemyDeploying = true;
                shieldRotationTimer = 0;
            }
        }
        else
        {
            if (enemyDeploying)
            {
                //Debug.Log("Waiting to deploy");
                // Wait until new enemies can be deployed
                float currentSpawnTime;
                if(enemiesDeployed == 0)
                    currentSpawnTime = 0;
                else
                    currentSpawnTime = enemySpawnTime;

                enemySpawnTimer += Time.deltaTime;
                if (enemySpawnTimer > currentSpawnTime)
                {
                    //Debug.Log("Spawning enemy");
                    GameObject myEnemy = Instantiate(currentEnemy, shieldTransform.position, shieldTransform.rotation);
                    Enemy enemyScript = myEnemy.GetComponent<Enemy>();
                    enemyScript.origin = shieldTransform;
                    enemiesDeployed++;

                    if (enemiesDeployed == enemiesPerDeploy)
                    {
                        enemyDeploying = false;
                        enemiesDeployed = 0;
                        enemySpawnTimer = 0;
                    }

                    enemySpawnTimer -= enemySpawnTime;
                }
            }
            else
            {
                // Wait until shield can rotate
                shieldRotationTimer += Time.deltaTime;
                if (shieldRotationTimer > shieldRotationTime)
                {
                    shieldNewRotation = new Vector3(0f, 0f, Random.Range(0f, 360f));
                    shieldRotating = true;
                }
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

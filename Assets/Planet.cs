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
    private sbyte _orbitPoints = 72;
    private Vector3[] _points;

    // Shield work variables
    private float _shieldRotationTimer = 0f;
    private bool _shieldRotating = false;
    private Vector3 _shieldNewRotation;

    // Enemyspawner work variables
    private float _enemySpawnTimer = 0f;
    private bool _enemyDeploying = false;
    private short _enemiesDeployed = 0;

    // Rabbit hole
    void Start()
    {
        drawOrbit();

        _enemySpawnTimer = enemySpawnTime; // So the first enemy comes out immediately.
    }

    // Cultist persistency
    void Update()
    {
        if (_shieldRotating)
        {
            shieldTransform.rotation = Quaternion.Lerp(shieldTransform.rotation, Quaternion.Euler(_shieldNewRotation), Time.deltaTime * shieldSpeed);

            float shieldCurrRot = Mathf.Abs(Mathf.Round(shieldTransform.rotation.z * 100) / 100);
            float shieldNewRot = Mathf.Abs(Mathf.Round(Quaternion.Euler(_shieldNewRotation).z * 100) / 100);
  
            if (shieldCurrRot == shieldNewRot)
            {
                _shieldRotating = false;
                _enemyDeploying = true;
                _shieldRotationTimer = 0;
            }
        }
        else
        {
            if (_enemyDeploying)
            {
                //Debug.Log("Waiting to deploy");
                // Wait until new enemies can be deployed
                _enemySpawnTimer += Time.deltaTime;
                if (_enemySpawnTimer > enemySpawnTime)
                {
                    //Debug.Log("Spawning enemy");
                    GameObject myEnemy = Instantiate(currentEnemy, shieldTransform.position, shieldTransform.rotation);
                    Enemy enemyScript = myEnemy.GetComponent<Enemy>();
                    enemyScript.origin = shieldTransform;
                    _enemiesDeployed++;

                    if (_enemiesDeployed == enemiesPerDeploy)
                    {
                        _enemyDeploying = false;
                        _enemiesDeployed = 0;
                        _enemySpawnTimer = enemySpawnTime; // So next time, first enemy deploys immediately.
                    }

                    _enemySpawnTimer -= enemySpawnTime;
                }
            }
            else
            {
                // Wait until shield can rotate
                _shieldRotationTimer += Time.deltaTime;
                if (_shieldRotationTimer > shieldRotationTime)
                {
                    _shieldNewRotation = new Vector3(0f, 0f, Random.Range(0f, 360f));
                    _shieldRotating = true;
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
        lineRenderer.positionCount = _orbitPoints;

        _points = new Vector3[_orbitPoints];

        for (int i = 0; i < _orbitPoints; i++)
        {
            _points[i] = new Vector3(this.transform.position.x + (7 * Mathf.Sin(Mathf.Deg2Rad * i * 360 / _orbitPoints)),
                                    this.transform.position.y + (4 * Mathf.Cos(Mathf.Deg2Rad * i * 360 / _orbitPoints)), 0);
        }
        lineRenderer.SetPositions(_points);

    }
}

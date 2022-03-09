using UnityEngine;
using System;
//using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public GameObject enemy;
        public int amount = 0;
    }

    public List<Wave> wave;
    public GameObject player;
    [SerializeField] private float _spawnRadius = 2f; // TODO: Calculate this instead.

    private int _waveNum = 0;
    private float _waveDelay = 3F;
    private float _waveTimer = 0f;
    private bool _newWaveAllowed = true;

    void Start()
    {

    }

    void Update()
    {
        if (!_newWaveAllowed)
        {
            _waveTimer += Time.deltaTime;
            if (_waveTimer >= _waveDelay)
            {
                _newWaveAllowed = true;
            }
        }
    }

    public bool getNewWave()
    {
        if (doNewWave())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool doNewWave()
    {

        // Do we have waves left?
        if (_waveNum < wave.Count && _newWaveAllowed)
        {
            // Give me an amount of enemies
            for (int i = 0; i < wave[_waveNum].amount; i++)
            {
                // TODO: Actual pooling
                Vector2 enemyPosition = this.transform.position;
                enemyPosition += UnityEngine.Random.insideUnitCircle.normalized * _spawnRadius;
                GameObject enemyGO = Instantiate(wave[_waveNum].enemy, enemyPosition, this.transform.rotation);
                Enemy enemyGOScript = enemyGO.GetComponent<Enemy>();
                enemyGOScript.target = player.transform;
            }
            
            // Move to the next wave
            _waveNum++;
            // add UI manager call to temporary text
            // Reset wave timer and disallow new waves
            _waveTimer = 0;
            _newWaveAllowed = false;
        
            return true;
        }
        else
        {
            _waveNum = 0; 
            return false;
        }

    }

}

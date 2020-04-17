using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public GameObject enemy;
        public int amount = 0;
    }

    public List<Wave> wave;
    private int waveNum = 0;

    void Update()
    {

    }

    public void getNewWave()
    {
        if(waveNum < wave.Count)
        {
            for(int i = 0; i < wave[waveNum].amount; i++)
            {
                GameObject go = Instantiate(wave[waveNum].enemy, this.transform.position, this.transform.rotation);
                // TODO: Actual pooling
            }

            waveNum++;
        }
        else
        {
            waveNum = 0; // TODO: maybe inform the lack of new waves by returning a boolean? 
        }
            
    }
}

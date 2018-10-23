using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersController : MonoBehaviour {

    [SerializeField]
    GameObject SpawnerPrefab;

    [SerializeField]
    int WaveNumber=3;

    [SerializeField]
    float WaveDuration=9.2f;

    // public float spawningTime

    // Use this for initialization
    void Start()
    {

        StartCoroutine(SpawnerControllerCoroutine());
        //   var spawningTime= FindObjectOfType<AsteroidSpawner>().AsteroidSpawningTime;

    }

    private IEnumerator SpawnerControllerCoroutine()
    {
     
        while (true)
        {   
       
           // if (CurrentWaveNumber > 2)
             

            yield return new WaitForSeconds(WaveDuration*WaveNumber); //czas trwania Wave
            Instantiate(SpawnerPrefab);

            //  CurrentWaveNumber++;
        }
    }

}

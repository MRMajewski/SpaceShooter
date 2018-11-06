using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersController : MonoBehaviour {

    [SerializeField]
    GameObject EnemySpawnerPrefab;

    [SerializeField]
    GameObject OverloadSpawnerPrefab;

    [SerializeField]
    GameObject AsteroidAngledSpawnerPrefab;

    [SerializeField]
    int OverloadWaveNumber=3;

    [SerializeField]
    int EnemyWaveNumber = 3;

    [SerializeField]
    int AsteroidAngledWaveNumber = 3;

    [SerializeField]
    float WaveDuration=9.2f;

    // public float spawningTime

    // Use this for initialization
    void Start()
    {
    /*  var  WaveDuration = FindObjectOfType<AsteroidWaveController>().WaveDuration+
            FindObjectOfType<AsteroidWaveController>().CooldownDuration+
            FindObjectOfType<AsteroidWaveController>().BreakDuration;

    */
        StartCoroutine(EnemySpawnerControllerCoroutine());
        StartCoroutine(OverloadSpawnerControllerCoroutine());
        StartCoroutine(AsteroidAngledSpawnerControllerCoroutine());
        //   var spawningTime= FindObjectOfType<AsteroidSpawner>().AsteroidSpawningTime;

    }

    private IEnumerator EnemySpawnerControllerCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(WaveDuration * EnemyWaveNumber); //czas trwania Wave
            Instantiate(EnemySpawnerPrefab);
            yield return new WaitForSeconds(WaveDuration * 10); //czas trwania Wave
        }
       
    }

    private IEnumerator OverloadSpawnerControllerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(WaveDuration * OverloadWaveNumber); //czas trwania Wave
            Instantiate(OverloadSpawnerPrefab);
            {
                yield return new WaitForSeconds(WaveDuration * 15);
            }
        }
     

    }

    private IEnumerator AsteroidAngledSpawnerControllerCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(WaveDuration * AsteroidAngledWaveNumber); //czas trwania Wave
            Instantiate(AsteroidAngledSpawnerPrefab);
            yield return new WaitForSeconds(WaveDuration * 20);
        }
     

    }

}

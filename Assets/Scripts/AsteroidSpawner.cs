﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
   // [SerializeField]
  //  AsteroidType[] AsteroidTypes;

    [SerializeField]
    GameObject[] AsteroidTypes;


    [SerializeField]
    public float AsteroidSpawningTime = 2f;

    public int AsteroidTypeLevel { get; set; } //poziom trudności tak jakby
    public int AsteroidTypeRange { get; set; } //'zakres' asteroidów zależny od trudnosci


    public bool Spawning = true;

    // Use this for initialization
    void Start() {
        AsteroidTypeLevel = 0;
        AsteroidTypeRange = 2;

        StartCoroutine(SpawningCoroutine());
    }

    IEnumerator SpawningCoroutine()
    {
        int currentWave = FindObjectOfType<AsteroidWaveController>().CurrentWaveNumber;

        yield return new WaitForSeconds(1.5f);

        while (true)//nieskończona pętla zapewnia że korutyna się nie skończy
        {
            while (Spawning)//sprawdzamy czy asteroidy mają być generowane
            {
                SpawnAsteroid();
                yield return new WaitForSeconds(AsteroidSpawningTime);


            }

            yield return new WaitForEndOfFrame();//czekamy do następnej klatki żeby nie obciążać cpu

        }
    }

    private int GetRandomAsteroidType()  //pobieramy z tablicy indeks asteroidy
    {
        var index = AsteroidTypeLevel + Random.Range(-AsteroidTypeRange, AsteroidTypeRange);
        index = Mathf.Clamp(index, 0, AsteroidTypes.Length - 1); //upewniamy się że index nie wyjdzie poza zakres

        return index;
    }


    private void SpawnAsteroid()
    {

        

        var obj = Instantiate(AsteroidTypes[GetRandomAsteroidType()], transform.position, Quaternion.identity);
        obj.transform.position += Vector3.right * Random.Range(-2f, 2f);

        //var asteroidType = GetRandomAsteroidType(); 
      //  obj.GetComponent<Asteroid>().Configure(asteroidType); //konfigurujemy asteroide
    }
}

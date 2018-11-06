using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingEnemySpawner : MonoBehaviour
{
    [SerializeField]
    EnemyType[] EnemyTypes;

    [SerializeField]
    GameObject EnemyPrefab;

    [SerializeField]
    public float EnemySpawningTimeMin = 2f;

    [SerializeField]
    public float EnemySpawningTimeMax = 2f;

    //  public bool Spawning = true;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(SpawningCoroutine());
    }

    IEnumerator SpawningCoroutine()
    {
        int currentWave = FindObjectOfType<AsteroidWaveController>().CurrentWaveNumber;

        while (true)//nieskończona pętla zapewnia że korutyna się nie skończy
        {
            while (true)//sprawdzamy czy asteroidy mają być generowane
            {
                SpawnEnemy();
                var EnemySpawningTime = Random.Range(EnemySpawningTimeMin, EnemySpawningTimeMax);
                yield return new WaitForSeconds(EnemySpawningTime);


            }

            yield return new WaitForEndOfFrame();//czekamy do następnej klatki żeby nie obciążać cpu

        }
    }

    private EnemyType GetRandomEnemyType()  //pobieramy z tablicy indeks asteroidy
    {
        var index = Random.Range(0, EnemyTypes.Length);
        index = Mathf.Clamp(index, 0, EnemyTypes.Length - 1); //upewniamy się że index nie wyjdzie poza zakres

        return EnemyTypes[index];
    }

    private void SpawnEnemy()
    {
        var obj = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        obj.transform.position += Vector3.right * Random.Range(-2f, 2f);

        var enemyType = GetRandomEnemyType();
        obj.GetComponent<HomingEnemy>().Configure(enemyType); //konfigurujemy asteroide
    }
}

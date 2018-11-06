using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverloadSpawner : MonoBehaviour {

    [SerializeField]
    GameObject OverloadPrefab;

    [SerializeField]
    float MinSpawnTime=7f;

    [SerializeField]
    float MaxSpawnTime = 15f;


    // Use this for initialization
    void Start () {
        StartCoroutine(SpawningCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawningCoroutine()
    {

        while (true)//nieskończona pętla zapewnia że korutyna się nie skończy
        {
            while (true)//sprawdzamy czy asteroidy mają być generowane
            {
                SpawnOverload();
                yield return new WaitForSeconds(Random.Range(MinSpawnTime, MaxSpawnTime));
            }
            yield return new WaitForEndOfFrame();//czekamy do następnej klatki żeby nie obciążać cpu

        }
    }

    private void SpawnOverload()
    {
        var obj = Instantiate(OverloadPrefab, transform.position, Quaternion.identity);
        obj.transform.position += Vector3.right * Random.Range(-2f, 2f);

    }

}

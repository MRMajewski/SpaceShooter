using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidWaveController : MonoBehaviour
{

    public int CurrentWaveNumber { get; private set; }


    [SerializeField]
    float WaveDuration = 20f;

    [SerializeField]
    float CooldownDuration = 1f;

    [SerializeField]
    float BreakDuration = 5f;

    // public float spawningTime;

    public event System.Action<int> OnWaveStarted;
    public event System.Action<int> OnWaveEnded;


    // Use this for initialization
    void Start()
    {
        CurrentWaveNumber = 1;

        StartCoroutine(AsteroidWaveControllerCoroutine());
        //   var spawningTime= FindObjectOfType<AsteroidSpawner>().AsteroidSpawningTime;

    }

    private IEnumerator AsteroidWaveControllerCoroutine()
    {
        var spawner = FindObjectOfType<AsteroidSpawner>();
        //  var spawningTime = FindObjectOfType<AsteroidSpawner>().AsteroidSpawningTime;



        while (true)
        {
            if (OnWaveStarted != null)
                OnWaveStarted.Invoke(CurrentWaveNumber);

            spawner.AsteroidTypeLevel = CurrentWaveNumber;



            spawner.Spawning = true; //włączamy tworzenie asteroidów tzn 'Wave"

            yield return new WaitForSeconds(WaveDuration); //czas trwania Wave

            spawner.Spawning = false; //koniec fali

            yield return new WaitForSeconds(CooldownDuration);

            if (OnWaveEnded != null)
                OnWaveEnded.Invoke(CurrentWaveNumber);
            yield return new WaitForSeconds(BreakDuration); // czas trwania przerwy

            CurrentWaveNumber++;
            //spawningTime = spawningTime + 3f;



        }
    }
}
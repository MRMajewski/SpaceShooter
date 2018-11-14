using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ShipShield : MonoBehaviour, IUpgradable
{

    private AudioSource AudioSource;

    [SerializeField]
    Sprite[] ShieldStates;

    [SerializeField]
    AudioClip ShieldClip;



    private void Update()
    {
        gameObject.transform.position = FindObjectOfType<Ship>().transform.position;
    }



    bool Active
    {
        get { return CurrentLevel > 0; }
    }

    #region


   public int MaxLevel
    {
        get { return ShieldStates.Length - 1; }
    }

    public  int CurrentLevel
    {
        get;private set;

    }

    public int UpgradeCost {
        get { return CurrentLevel * 75 + 200; }
    }

    public void Upgrade()
    {
        CurrentLevel++;
        Rebuild();
    }
    #endregion

    int currentState = 0;
    public int CurrentState
    {
        get { return currentState; }
        set
        {
            currentState = Mathf.Clamp(value, 0, MaxLevel);
            UpdateSprite();
        }
    }

    private void Awake()
    {
        // FindObjectOfType<AsteroidWaveController>().OnWaveStarted += _ => Rebuild();
        AudioSource= GetComponent<AudioSource>();
        AudioSource.clip = ShieldClip;

    }

    private void Start()
    {
       Rebuild();
    }

    public void Rebuild()
    {
        CurrentState = CurrentLevel;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sprawdzamy czy zderzenie nastąpiło z asteroidą, poniżej pobieramy komponent
        var asteroid = collision.gameObject.GetComponent<Asteroid>();
        var enemy = collision.gameObject.GetComponent<HomingEnemy>();
        var overload = collision.gameObject.GetComponent<Overload>();

        if (asteroid == null &&
            enemy==null)
            return;
        if (overload != null) return;

        if (!Active) //jeśli nieaktywna to nic sie nie dzieje
            return;

        CurrentState--;
        CurrentLevel--;

        if(asteroid !=null) Destroy(asteroid.gameObject);
        if(enemy !=null) Destroy(enemy.gameObject); ;

        AudioSource.Play();
    }

     private void UpdateSprite()
        {
            GetComponent<SpriteRenderer>().sprite = ShieldStates[CurrentState]; //wyłączamy sprite tarczy
        }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ShipShield : MonoBehaviour, IUpgradable
{

    [SerializeField]
    Sprite[] ShieldStates;




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
        get { return CurrentLevel * 50 + 100; }
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
        FindObjectOfType<AsteroidWaveController>().OnWaveStarted += _ => Rebuild();
    }

    private void Start()
    {
        Rebuild();
    }

    public void Rebuild()
    {
        CurrentState = CurrentState;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sprawdzamy czy zderzenie nastąpiło z asteroidą, poniżej pobieramy komponent
        var asteroid = collision.gameObject.GetComponent<Asteroid>();

        if (asteroid == null)
            return;

        if (!Active) //jeśli nieaktywna to nic sie nie dzieje
            return;

        CurrentState--;
        Destroy(asteroid.gameObject);
    }

     private void UpdateSprite()
        {
            GetComponent<SpriteRenderer>().sprite = ShieldStates[CurrentLevel]; //wyłączamy sprite tarczy
        }

}

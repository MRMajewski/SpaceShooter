using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ShipShield : MonoBehaviour {

    [SerializeField]
    Sprite[] ShieldStates;

    private void Update()
    {
        gameObject.transform.position = FindObjectOfType<Ship>().transform.position;
    }


    int MaxLevel
    {
        get { return ShieldStates.Length - 1; }
    }

    int currentLevel = 0;
    int CurrentLevel
    {
        get { return currentLevel; }
        set
        {
            currentLevel = Mathf.Clamp(value, 0 , MaxLevel);
            UpdateSprite();
        }
    }

    bool Active
    {
        get { return CurrentLevel > 0; }
    }

    private void Start()
    {
        CurrentLevel = MaxLevel;
   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sprawdzamy czy zderzenie nastąpiło z asteroidą, poniżej pobieramy komponent
        var asteroid = collision.gameObject.GetComponent<Asteroid>();

        if (asteroid == null)
            return;

        if (!Active) //jeśli nieaktywna to nic sie nie dzieje
            return;

        CurrentLevel--;
        Destroy(asteroid.gameObject);
    }

     private void UpdateSprite()
        {
            GetComponent<SpriteRenderer>().sprite = ShieldStates[CurrentLevel]; //wyłączamy sprite tarczy
        }
}

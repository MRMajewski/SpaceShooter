﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AsteroidType
{
    public Sprite Sprite;
    public float Durability;

    public int Points = 5;

}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Asteroid : MonoBehaviour {

    [SerializeField]
    float Durability = 6f;

    [SerializeField]
    GameObject DestroyingParticles;
    [SerializeField]
    GameObject DestroyedParticles;

    private int Points;

    private SpriteRenderer SpriteRenderer;

    // Use this for initialization
    void Awake ()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        SetSpeed();
    }


    public void Configure(AsteroidType asteroidType)
    {
        GetComponent<SpriteRenderer>().sprite = asteroidType.Sprite;
       
        Durability = asteroidType.Durability;

        Points = asteroidType.Points;
    }

    public void SetSpeed()
    {
        var targetSpeed = Random.Range(2f, 4f);
        GetComponent<Rigidbody2D>().velocity = Vector2.down * targetSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject; //sprawdzamy typ obiektu kolizji czy posiada komponent Bullet tzn skrypt
        var bullet = obj.GetComponent<Bullet>();// przypisujemy obiekt pocisku do obj

        if (bullet != null)
        {
            //tworzymy efekt cząsteczkowy gdy asteroida jest trfiona, w miejscu kolizji
            GenerateParticles(DestroyingParticles, collision.transform.position);

            DecreaseDurability(bullet.Power);

            Destroy(obj);
        }
    }

    private void DecreaseDurability(float amount)
    {

        Durability -= amount; //zmniejszamy hpki asteroidy o moc pocisku


        if (Durability <= 0)
        {
            //tworzymy efekt cząsteczkowy gdy asteroida jest zniszczona
            GenerateParticles(DestroyedParticles, transform.position);

            FindObjectOfType<GameManager>().Money += Points;

            Destroy(gameObject);

        }
           
    }

    private void GenerateParticles(GameObject prefab, Vector3 position)
    {

        var particles = Instantiate(prefab, position, Quaternion.identity);
        particles.GetComponent<ParticleSystemRenderer>().material.mainTexture =
           SpriteRenderer.sprite.texture;
    }
}
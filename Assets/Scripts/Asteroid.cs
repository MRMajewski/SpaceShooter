using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

[System.Serializable]
public class AsteroidType
{
    public Sprite Sprite;
  //  public PolygonCollider2D PolygonCollider;
    public float Durability;

    public int Points = 5;

}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Asteroid : MonoBehaviour {

    [SerializeField]
    AudioClip DestroyClip;

    private AudioSource AudioSource;

    [SerializeField]
    float Durability = 6f;

    [SerializeField]
    GameObject DestroyingParticles;
    [SerializeField]
    GameObject DestroyedParticles;

    [SerializeField]
    float AngleMin = 0f;

    [SerializeField]
    float AngleMax = 0f;

    private int Points;

    private SpriteRenderer SpriteRenderer;

    // Use this for initialization
    void Awake ()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        SetSpeed();

        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = DestroyClip;
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
        float Angle= Random.Range(AngleMin, AngleMax);
        GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0f,0f,Angle) * Vector2.down * targetSpeed;
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
            StartCoroutine(DestroyCoroutine());

        }
           
    }

    private void GenerateParticles(GameObject prefab, Vector3 position)
    {

        var particles = Instantiate(prefab, position, Quaternion.identity);
        particles.GetComponent<ParticleSystemRenderer>().material.mainTexture =
           SpriteRenderer.sprite.texture;
    }

    IEnumerator DestroyCoroutine()
    {
        GetComponent<AudioSource>().Play(); //efekt dźwiękowy

        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        //tworzymy efekt cząsteczkowy gdy asteroida jest zniszczona
        GenerateParticles(DestroyedParticles, transform.position);

        GetComponent<Renderer>().enabled = false; //wyłączamy render i colider
        GetComponent<Collider2D>().enabled = false;

        FindObjectOfType<GameManager>().Money += Points;

        CameraShaker.Instance.ShakeOnce(3, 7, 0.5f, 0.5f);
        Destroy(gameObject, DestroyClip.length); //dopiero jak skończy się efekt, obiekt jest niszczony

    }

}

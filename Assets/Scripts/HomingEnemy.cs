using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;



[System.Serializable]
public class EnemyType
{
    public Sprite Sprite;
    //  public PolygonCollider2D PolygonCollider;
    public float Durability;

    public int Points = 5;

    public float Speed = 1f;

}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class HomingEnemy : MonoBehaviour {

    Rigidbody2D Rigidbody;
    Vector2 TargetPosition;
  //  Player TargetPlayer;


    Ship ship;
  
    [SerializeField]
    AudioClip DestroyClip;

    private AudioSource AudioSource;

    [SerializeField]
    float Durability = 6f;

    [SerializeField]
    GameObject DestroyingParticles;
    [SerializeField]
    GameObject DestroyedParticles;


    private int Points;



    private SpriteRenderer SpriteRenderer;

    float targetSpeed;
    

    // Use this for initialization
    void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = DestroyClip;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
       ship = FindObjectOfType<Ship>();
      // targetSpeed = Random.Range(1f, 2.5f);
    }
	
	// Update is called once per frame
	void Update () {
    
        transform.position = Vector2.MoveTowards(transform.position, ship.transform.position, targetSpeed * Time.deltaTime);

        var direction = (Vector3)ship.transform.position - transform.position;
        var targetVelocity = direction.normalized * targetSpeed; //otrzymujemy wektor jednostkowy znormalizowany

        Rigidbody.velocity = Vector3.Lerp(Rigidbody.velocity, targetVelocity, Time.deltaTime / 2f);

        transform.up = (Vector2)direction;//zombie będzie zwrócony w kierunku którym idzie
    }

    public void Configure(EnemyType enemyType)
    {
        GetComponent<SpriteRenderer>().sprite = enemyType.Sprite;

        Durability = enemyType.Durability;

        Points = enemyType.Points;

        targetSpeed = enemyType.Speed;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Collider2D))]

public class Overload : MonoBehaviour
{

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Button Overloadbutton;



    //  [SerializeField]
    //  Button OverloadButton;


    /*   private void OnTriggerEnter2D(Collider2D collision)
       {
           var obj = collision.gameObject; //sprawdzamy typ obiektu kolizji czy posiada komponent Bullet tzn skrypt
           var bullet = obj.GetComponent<Bullet>();// przypisujemy obiekt pocisku do obj

           if (bullet != null)
           {
               OverloadButton.GetComponent<SpriteRenderer>().color = Color.red;
               Destroy(gameObject);
               Destroy(obj);
           }

           }
       }
       */

    void Awake()
    {
        SetSpeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject; //sprawdzamy typ obiektu kolizji czy posiada komponent Bullet tzn skrypt
        var bullet = obj.GetComponent<Bullet>();// przypisujemy obiekt pocisku do obj

        if (bullet != null)
        {
        //    
            Destroy(gameObject);
            Destroy(obj);

            //    canvas.enabled = true;

            GetComponent<Button>().gameObject.SetActive(true);

          //  Overloadbutton.interactable = true;
          //  FindObjectOfType<OverloadButton>().

            //    FindObjectOfType<Ship>().GetComponent<SpriteRenderer>().color    = Color.black; to działa ale enabled buttona nie ;/
        }
    }


    public void SetSpeed()
    {
        var targetSpeed = Random.Range(2f, 4f);
        float Angle = Random.Range(-5, 5);
        GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0f, 0f, Angle) * Vector2.down * targetSpeed;
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipMovement : MonoBehaviour {


    [SerializeField]
    float MaxPositionX = 5f;

    [SerializeField]
    float MaxPositionY = 5f;

   // [SerializeField]
   // Vector2 MovementArea;

    [SerializeField]
    Joystick joystick;

    Camera Camera;

    // Use this for initialization
    void Start() {
        Camera = FindObjectOfType<Camera>();

    }

    private void OnDrawGizmos()
    {
        //położenie + wielkość
    //    Gizmos.DrawWireCube(Vector3.up*2.8f, MovementArea * 2f);
    }


    // Update is called once per frame
    void Update() {
        //odczytujem połozenie myszy i konwertujemy je na położenie w świecie gry 3d


        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.up * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {

            transform.Translate(moveVector * Time.deltaTime * 4f, Space.World);

            var positionX = Mathf.Clamp(transform.position.x, -MaxPositionX, MaxPositionX);
           

            var positionY = Mathf.Clamp(transform.position.y, -MaxPositionY, MaxPositionY);
            //funkcja Clamp zwraca daną wartość z  określonego przedziału
            //jeżeli ta wartość x zawiera się w przedziale to jest zwracana, w innym przypadku zwracana jest skrajna wartość

            transform.position = new Vector3(positionX, positionY);

           
            // transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }

        /* 
         *   if (moveVector != Vector3.zero)
          { 
              transform.rotation = Quaternion.LookRotation(Vector3.forward, moveVector);
              transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
          }
         
         * if (!(Input.GetMouseButton(0))) return;



          if (!(EventSystem.current.IsPointerOverGameObject()))
          { 
          var targetPosition = (Vector2)Camera.ScreenToWorldPoint(Input.mousePosition);

          //ograniczamy pole manewru statku, żeby nie wyleciał za plansze
          targetPosition.x = Mathf.Clamp(targetPosition.x, -MovementArea.x, MovementArea.x);
          targetPosition.y = Mathf.Clamp(targetPosition.y, -MovementArea.y, MovementArea.y);

          transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
          */
    }


}



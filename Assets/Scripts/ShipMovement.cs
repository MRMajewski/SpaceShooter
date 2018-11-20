using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipMovement : MonoBehaviour {

    [SerializeField]
    Vector2 MovementArea;

    Camera Camera;

    // Use this for initialization
    void Start() {
        Camera = FindObjectOfType<Camera>();

    }

    private void OnDrawGizmos()
    {
        //położenie + wielkość
        Gizmos.DrawWireCube(Vector3.up*2.8f, MovementArea * 2f);
    }


    // Update is called once per frame
    void Update() {
        //odczytujem połozenie myszy i konwertujemy je na położenie w świecie gry 3d


        if (!(Input.GetMouseButton(0))) return;

      

        if (!(EventSystem.current.IsPointerOverGameObject()))
        { 
        var targetPosition = (Vector2)Camera.ScreenToWorldPoint(Input.mousePosition);

        //ograniczamy pole manewru statku, żeby nie wyleciał za plansze
        targetPosition.x = Mathf.Clamp(targetPosition.x, -MovementArea.x, MovementArea.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, -MovementArea.y, MovementArea.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
    }
        

    }

}

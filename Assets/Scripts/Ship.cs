using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {


    public event System.Action OnShipDestroyed;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var asteroid = collision.gameObject.GetComponent<Asteroid>();

        if (asteroid == null)
            return;

        if (OnShipDestroyed != null)
            OnShipDestroyed.Invoke();
    }
}

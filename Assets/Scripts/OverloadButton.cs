using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverloadButton : MonoBehaviour {

     ShipGun shoot;

   // [SerializeField]
  //  Canvas canvas;

 //   [SerializeField]
  //  Button Overloadbutton;



    // Use this for initialization
    void Awake () {
        shoot = FindObjectOfType<ShipGun>();
        //canvas.enabled = false;
        //  GetComponent<Button>().interactable = false;
        // GetComponent<Button>().gameObject.SetActive(false);
        GetComponent<Button>().interactable = false;
        // Overloadbutton = GetComponent<Button>();

        //  Overloadbutton.gameObject.SetActive(false);
    }
	
    void  Start()
    {
        // GetComponent<OverloadButton>().enabled = false;
    }
	// Update is called once per frame
	void Update () {
		
	}

  /*  public void DoIT()
    {
     
     
        StartCoroutine(OverloadCoroutine());
        //  canvas.enabled = false;


        // Overloadbutton.gameObject.SetActive(false);

        // GetComponent<Button>().interactable = false;
        GetComponent<Button>().gameObject.SetActive(false);
    } */

    public void DoIT()
    {


        StartCoroutine(OverloadCoroutine());
        //  canvas.enabled = false;

      //  GetComponent<Button>().gameObject.SetActive(false);
         GetComponent<Button>().interactable = false;
    }


    IEnumerator OverloadCoroutine()
    {


        for (int i = 0; i < 7; i++)
        {
            ShotOverload();

            yield return new WaitForSeconds(0.25f);
        }

    }
    public void ShotOverload()
    {

        shoot.ShootBullet(Vector3.down * 0.8f, Vector3.zero);
        shoot.ShootBullet(Vector3.left * 0.4f, Vector3.down * 0.8f, Vector3.forward * 2f);
        shoot.ShootBullet(Vector3.right * 0.4f, Vector3.down * 0.8f, Vector3.back * 2f);
        shoot.ShootBullet(Vector3.left * 0.8f, Vector3.down * 0.8f, Vector3.forward * 2f);
        shoot.ShootBullet(Vector3.right * 0.8f, Vector3.down * 0.8f, Vector3.back * 2f); ;
    }
}

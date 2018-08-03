using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour {

    [SerializeField]
    GameObject BullePrefab;

    [SerializeField]
    BulletType BulletType;


    float LastShootTime = 0f;
	
	// Update is called once per frame
	void Update ()
    {
        if (!Input.GetMouseButton(0))
            return;
      

        if (!CanShootBullet())
            return;

        ShootBullets();
        LastShootTime = Time.timeSinceLevelLoad; //resetujemy czas strzału
    }

    private void ShootBullets()
    {
        if(BulletType.CannonType==CannonType.Single)
        ShootBullet(Vector3.zero, Vector3.zero);

       else if(BulletType.CannonType == CannonType.Double)
        {
            ShootBullet(Vector3.left *0.1f,Vector3.forward *5f);
            ShootBullet(Vector3.right * 0.1f, Vector3.back * 5f);
        }

       else if(BulletType.CannonType == CannonType.Triple)
        {
            ShootBullet(Vector3.zero, Vector3.zero);
            ShootBullet(Vector3.left * 0.1f, Vector3.forward * 15f);
            ShootBullet(Vector3.right * 0.1f, Vector3.back * 15f);

        }
    }

    private void ShootBullet(Vector3 position, Vector3 rotation)
    {
        //tworzymy obiekt i umieszczamy go na dziobie statku
        var bullet = Instantiate(
            BullePrefab,
            transform.position+position+Vector3.up *0.5f,
            Quaternion.Euler(rotation));


        bullet.GetComponent<Bullet>().Configure(BulletType);

   
    }

    private bool CanShootBullet()
    {
        return (Time.timeSinceLevelLoad - LastShootTime > BulletType.ShootingDuration);
        //mierzymy różnicę między czasem od początku gry do czasu ostatniego strzału
    }
}

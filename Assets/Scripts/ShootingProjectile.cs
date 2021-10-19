using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject ShootingPoint;
    public float shootSpeed = 1000;
    public float fireRate = 0.4F;
    private float nextFire = 0.0F;
    void Start()
    {
        
    }

    void Update()
    {
        //shooting bullets
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            //fire rate
            nextFire = Time.time + fireRate;
            shootBullet();
        }
    }
    void shootBullet()
    {
        GameObject tempObj;
        //Instantiate/Create Bullet
        tempObj = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        tempObj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, shootSpeed, 0));


    }
}

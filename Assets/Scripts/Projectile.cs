using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool collided;

    void OnCollisionEnter(Collision co)
    {
    //destroying bullet on hit
        if(co.gameObject.tag != "Bullet" && co.gameObject.tag != "Player" && co.gameObject.tag != "ground" && !collided)
        {
          
            collided = true;
            Destroy(gameObject);
           
        }
    //Destroying other objects with bullet
       if(co.gameObject.tag == "Enemy")
        {
            Destroy(GameObject.FindWithTag("Enemy"));
        }
    }
}

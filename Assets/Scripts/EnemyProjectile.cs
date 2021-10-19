using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private bool collided;

    void OnCollisionEnter(Collision col)
    {
        //destroying bullet on hit
        if (col.gameObject.tag != "EnemyBullet" && col.gameObject.tag != "Enemy"  && !collided)
        {

            collided = true;
            Destroy(gameObject);

        }
        //Destroying other objects with bullet
        if (col.gameObject.tag == "Player")
        {
            Destroy(GameObject.FindWithTag("Player"));
        }
    }
}

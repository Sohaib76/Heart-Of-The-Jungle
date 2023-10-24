using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionWithEnemy : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an enemy.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy GameObject.
            Destroy(collision.gameObject);

            // Destroy the projectile as it has hit an enemy.
            Destroy(gameObject);
        }
    }
}

using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;
using System.Collections;
using System.Collections.Generic;




namespace Platformer.Gameplay
{
    public class PlayerScare : MonoBehaviour
    {
        public float scareRange = 5.0f; // Range within which enemies get scared.
        public KeyCode scareKey = KeyCode.Q; // The key to press to scare enemies.
        public EnemyController enemy;
        public GameObject enemyObject;

        public GameObject projectilePrefab; // The prefab of the projectile.
        public float throwForce = 10f;

        public PlayerTokenCollision playerTokenCollisionScript; // Reference to PlayerTokenCollision.

        public PlayerController playerController;

        private Animator playerAnimator;
        



        // Rest of your script.

        private void Start()
        {
            playerAnimator = GameObject.Find("Player").GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(scareKey))
            {
                ScareEnemies();
            }
            if (Input.GetKeyDown(KeyCode.X) && GameController.projectileCount >= 1 && playerController.controlEnabled)
            {
                GameController.projectileCount--;
                ThrowProjectile();
            }
        }

        private void ScareEnemies()
        {
            int enemyLayerMask = LayerMask.GetMask("Enemy");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 5, enemyLayerMask);

            if (hit.collider != null)
            {
                Debug.Log("Ray Hit Collider: " + hit.collider);
                // Get the enemy GameObject and its Rigidbody2D.
                GameObject enemyObject = hit.collider.gameObject;
                Rigidbody2D enemyRigidbody = enemyObject.GetComponent<Rigidbody2D>();
                Animator trollAnimator = enemyObject.GetComponent<Animator>();

                playerAnimator.SetTrigger("playScareAnim");
                trollAnimator.SetTrigger("trollScared");
                // Define the movement speed.
                float movementSpeed = 1.0f; // Adjust the speed as needed.

                // Start a coroutine to move the enemy slowly.
                StartCoroutine(MoveEnemySlowly(enemyRigidbody, movementSpeed));


                Debug.DrawRay(transform.position, transform.right * 100, Color.red);
            }

            
            else
            {
                Debug.Log("In else statement");
                //Schedule<PlayerDeath>();
            }

            // Coroutine to move the enemy slowly.
            IEnumerator MoveEnemySlowly(Rigidbody2D enemyRigidbody, float speed)
            {
                // Move the enemy for a certain duration.
                float duration = 2.0f; // Adjust the duration as needed.
                float elapsedTime = 0.0f;

                while (elapsedTime < duration)
                {
                    // Move the enemy to the right.
                    enemyRigidbody.velocity = new Vector2(speed, 0);

                    // Update the elapsed time.
                    elapsedTime += Time.deltaTime;

                    yield return null;
                }

                // Stop the enemy's movement.
                enemyRigidbody.velocity = Vector2.zero;


                Destroy(enemyObject);
                //enemyObject.SetActive(false);

            }

        }



        private void ThrowProjectile()
        {
            // Instantiate a new projectile.
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Access the Rigidbody2D component of the projectile.
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // Determine the direction the player is facing (e.g., to the right).
            Vector2 throwDirection = transform.right;

            // Apply a force to the projectile in the throw direction.
            rb.velocity = throwDirection * throwForce;

            // Destroy the projectile after a certain time to prevent memory leaks (you can adjust the time as needed).
            Destroy(projectile, 5f);
        }

    }
    
}
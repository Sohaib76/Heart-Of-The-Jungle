using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPath : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Adjust the speed as needed
    private bool facingRight = true;

    private void Update()
    {
        // Call the MoveLeft function in the Update method to make the enemy move left.
        MoveLeft();
    }

    private void MoveLeft()
    {
        // Calculate the movement direction based on the current facing direction.
        Vector2 movement = facingRight ? Vector2.right : Vector2.left;
        movement *= moveSpeed * Time.deltaTime;

        // Apply the movement to the enemy's Rigidbody2D.
        GetComponent<Rigidbody2D>().velocity = movement;

        // Flip the enemy's sprite if it hits a wall or platform.
        if (facingRight && IsBlockedLeft())
        {
            Flip();
        }
        else if (!facingRight && IsBlockedRight())
        {
            Flip();
        }
    }

    private bool IsBlockedLeft()
    {
        // Implement your own logic here to check if there is an obstacle or platform on the left.
        // You can use Physics2D.Raycast or other methods for this check.
        // Return true if blocked, false if not.
        return false; // Change this to your collision detection logic.
    }

    private bool IsBlockedRight()
    {
        // Implement your own logic here to check if there is an obstacle or platform on the right.
        // You can use Physics2D.Raycast or other methods for this check.
        // Return true if blocked, false if not.
        return false; // Change this to your collision detection logic.
    }

    private void Flip()
    {
        // Flip the enemy's sprite and change the facing direction.
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

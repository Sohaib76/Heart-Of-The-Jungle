using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlayer : MonoBehaviour
{
    public string interactKey = "e"; // The key to press for interaction (default is "e")
    public Collider2D playerCollider; // Reference to the player's Collider2D
    public GameObject playerObject; // Reference to the player's GameObject

    private bool canHide = false;
    private bool isHidden = false;


    private void Update()
    {
        if (canHide && Input.GetKeyDown(interactKey))
        {
            if (playerCollider != null && playerObject != null)
            {
                isHidden = !isHidden;
                // Disable the entire player object
                playerObject.SetActive(!isHidden);
                // You can add additional logic here, like playing an animation or sound
                // to indicate that the object is hidden.
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            canHide = true;
            // You can add code to display a prompt or message to inform the player that
            // they can press 'E' to hide.
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            canHide = false;
            // You can add code to remove the prompt or message when the player moves away.
        }
    }
}

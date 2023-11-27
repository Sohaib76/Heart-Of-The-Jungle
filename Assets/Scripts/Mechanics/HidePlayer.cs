using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HidePlayer : MonoBehaviour
{
    public string interactKey = "e"; // The key to press for interaction (default is "e")
    public Collider2D playerCollider; // Reference to the player's Collider2D
    public GameObject playerObject; // Reference to the player's GameObject

    private bool canHide = false;
    private bool isHidden = false;

    internal Animator animator;

    void Awake()
    {
        animator = playerObject.GetComponent<Animator>();
    }


    private async void Update()
    {
        if (canHide && Input.GetKeyDown(interactKey))
        {
            if (playerCollider != null && playerObject != null)
            {
                isHidden = !isHidden;
                StartCoroutine(TogglePlayerVisibility(isHidden));
            }
        }
    }

    private IEnumerator TogglePlayerVisibility(bool isHidden)
    {
        if (isHidden)
        {
            Debug.Log("Player hiding animation playing now");
            // start the animation
            animator.SetBool("hidden", isHidden);

            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
            // wait for the animation to stop playing
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            Debug.Log("Player hiding now");
            // disable the player object
            playerObject.SetActive(!isHidden);
        } else
        {
            Debug.Log("Player coming out of hiding now");
            // else enable the player obj and then start the animation
            playerObject.SetActive(!isHidden);
            animator.SetBool("hidden", isHidden);
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

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
    public AudioClip hideSound; // Audio clip for hiding
    public AudioClip revealSound; // Audio clip for coming out of hiding

    public PlayerController playerController;

    private bool isAnimationPlaying = false;

    private bool canHide = false;
    private bool isHidden = false;

    internal Animator animator;
    private AudioSource audioSource;

    public GameObject eyeObject;

    void Awake()
    {
        animator = playerObject.GetComponent<Animator>();
        audioSource = playerObject.GetComponent<AudioSource>();

    }


    private async void Update()
    {
        if (!isAnimationPlaying && canHide && Input.GetKeyDown(interactKey))
        {
            playerController.controlEnabled = !playerController.controlEnabled;
            if (playerCollider != null && playerObject != null)
            {
                isHidden = !isHidden;
                isAnimationPlaying = true; // Set flag to true when animation starts
                StartCoroutine(TogglePlayerVisibility(isHidden));
            }
        }
    }

    private IEnumerator TogglePlayerVisibility(bool isHidden)
    {
        if (isHidden)
        {
            Debug.Log("Player hiding animation playing now");

            if (hideSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hideSound);
            }


            // start the animation
            animator.SetBool("hidden", isHidden);

            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
            // wait for the animation to stop playing
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            Debug.Log("Player hiding now");
            // disable the player object
            playerObject.SetActive(!isHidden);
            eyeObject.SetActive(isHidden);
            isAnimationPlaying = false;
        } else
        {
            Debug.Log("Player coming out of hiding now");

            if (revealSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(revealSound);
            }
            // else enable the player obj and then start the animation
            playerObject.SetActive(!isHidden);
            eyeObject.SetActive(isHidden);
            animator.SetBool("hidden", isHidden);
            isAnimationPlaying = false;
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

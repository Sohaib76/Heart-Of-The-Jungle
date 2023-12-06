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

    public AudioSource audioSource;

    private bool canHide = false;
    public bool isHidden {get; private set;}= false;

    internal Animator animator;
    // private AudioSource audioSource;

    public GameObject eyeObject;

    public AudioSource targetAudioSource;

    void Awake()
    {
        animator = playerObject.GetComponent<Animator>();
        //audioSource = playerObject.GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (canHide && Input.GetKeyDown(interactKey))
        {
            playerController.controlEnabled = !playerController.controlEnabled;
            if (playerCollider != null && playerObject != null)
            {
                isHidden = !isHidden;
                if (isHidden)
                {
                    if (hideSound != null && audioSource != null)
                    {
                        //audioSource.PlayOneShot(hideSound);
                        //audioSource.Play(hideSound);
                        //audioSource.clip = hideSound;
                        audioSource.Play();
                        targetAudioSource.enabled = false;
                    }

                    Debug.Log("Player hiding now");
                    // disable the player object
                    playerObject.SetActive(!isHidden);
                    eyeObject.SetActive(isHidden);
                }
                else
                {
                    Debug.Log("Player coming out of hiding now");

                    if (revealSound != null && audioSource != null)
                    {
                        //audioSource.PlayOneShot(revealSound);
                        // audioSource.Play();
                        targetAudioSource.enabled = true;
                        audioSource.Stop();

                    }
                    // else enable the player obj and then start the animation
                    playerObject.SetActive(!isHidden);
                    eyeObject.SetActive(isHidden);
                    animator.SetBool("hidden", isHidden);
                }
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

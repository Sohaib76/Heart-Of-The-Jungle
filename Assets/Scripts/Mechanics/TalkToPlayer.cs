using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TalkToPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueCanvasPrefab;

    [SerializeField] float timeBtwnChars = 0.02f;
    [SerializeField] float timeBtwnWords = 1.0f;

    [SerializeField] float pandaPitchValue = 1.0f;
    [SerializeField] float owlPitchValue = 2.5f;

    public AudioClip dialogueSpeakClip;

    private AudioSource audioSource;

    public string interactKey = "e"; // The key to press for interaction (default is "e")

    public bool isLevel2 = false;

    // Hardcoded dialogue options
    private readonly Dictionary<int, string> beforeFindingScrollTalkWithOwl = new Dictionary<int, string>
    {
        
        {1, "Greetings, noble red panda. I am the keeper of ancient wisdom, and my scrolls have been stolen " +
            "by mischievous goblins. Will you embark on a quest to retrieve them and safeguard the " +
            "knowledge within?" },
        {2,  "I'm ready to help! What can you tell me about these goblins and the scrolls?"},
        {3,  "The goblins roam the depths of the jungle. Seek their lair and bring back the stolen " +
            "scrolls.The fate of our ancient knowledge rests upon your paws."},
    };

    private readonly Dictionary<int, string> beforeFindingScrollTalkWithOwl2 = new Dictionary<int, string>
    {

        {1, "Oh no! It seems the goblins haven't stopped playing their evil plan. An important page"
        + "of the scroll is ripped. I need to retrieve it. Can you help me?" },
        {2,  "Of course I can help! Let's do it!"},
        {3,  "Thank you for your help once again..."},
    };


    private string owlName = "Wise Owl";
    private string pandaName = "Rori the Panda";

    private int dialogueCounter = 0;
    private bool isTalking = false;
    private bool isNearby = false;

    private bool sentenceEnded = false;

    private TextMeshProUGUI dialogueTextBox;
    private TextMeshProUGUI speakerTextBox;

    private GameObject dialogueCanvas;
    private CanvasGroup canvasGroup;

    private GameObject pandaDialogueObject;
    private GameObject owlDialogueObject;

    private Vector3 oldOwlImagePosition;
    private Vector3 oldPandaImagePosition;

    private Color tintedColor;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate a new dialogue canvas for the script to use
        dialogueCanvas = Instantiate(dialogueCanvasPrefab, Vector3.zero, Quaternion.identity);
        dialogueCanvas.transform.SetParent(GameObject.Find("DialogueUI").transform);

        // Find and disable the canvas objects
        canvasGroup = dialogueCanvas.GetComponent<CanvasGroup>();

        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.3f;

        // Find all the relevant child game objects
        dialogueTextBox = dialogueCanvas.transform.Find("DialogueTextBox").GetComponent<TextMeshProUGUI>();
        speakerTextBox = dialogueCanvas.transform.Find("SpeakerTextBox").GetComponent <TextMeshProUGUI>();

        pandaDialogueObject = dialogueCanvas.transform.Find("DialogueTextBox/DialoguePandaImage").gameObject;
        owlDialogueObject = dialogueCanvas.transform.Find("DialogueTextBox/DialogueOwlImage").gameObject;

        oldOwlImagePosition = owlDialogueObject.transform.position;
        oldPandaImagePosition = pandaDialogueObject.transform.position;

        tintedColor = pandaDialogueObject.transform.GetComponent<UnityEngine.UI.Image>().color;

        // Set up the appropriate initial data
        //if (!isLevel2)
        //{
        //    dialogueTextBox.text = beforeFindingScrollTalkWithOwl[1];
        //}
        //else
        //{
        //    dialogueTextBox.text = beforeFindingScrollTalkWithOwl2[1];
        //}
        //dialogueTextBox.text = beforeFindingScrollTalkWithOwl[1];
        //speakerTextBox.text = owlName;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTalking && Input.GetKeyDown(interactKey))
        {
            if (isNearby)
            {
                isTalking = true;
                ShowCanvasGroup(true);

                // Highlight the owl first
                HighlightSpeaker(owlName);
                speakerTextBox.text = owlName;

                dialogueCounter++;
                if (!isLevel2)
                {
                    dialogueTextBox.text = beforeFindingScrollTalkWithOwl[dialogueCounter];
                }
                else
                {
                    dialogueTextBox.text = beforeFindingScrollTalkWithOwl2[dialogueCounter];
                }

                StartCoroutine(TextVisible());

                return;
            }
        }

        if (isTalking)
        {
            if (Input.GetKeyDown(interactKey) && isNearby)
            {
                dialogueCounter++;

                if (dialogueCounter > beforeFindingScrollTalkWithOwl.Keys.Max())
                {
                    dialogueCounter = 0;

                    ShowCanvasGroup(false);
                    isTalking = false;

                    return;
                }

                if (dialogueCounter % 2 == 0) // Panda is talking
                {
                    HighlightSpeaker(pandaName);
                    speakerTextBox.text = pandaName;
                }
                else // Owl is talking
                {
                    HighlightSpeaker(owlName);
                    speakerTextBox.text = owlName;
                }

                if (!isLevel2)
                {
                    dialogueTextBox.text = beforeFindingScrollTalkWithOwl[dialogueCounter];
                }
                else
                {
                    dialogueTextBox.text = beforeFindingScrollTalkWithOwl2[dialogueCounter];
                }

                StartCoroutine(TextVisible());
                //dialogueTextBox.text = beforeFindingScrollTalkWithOwl[dialogueCounter];
            }
        }

        if (!isNearby)
        {
            dialogueCounter = 0;

            ShowCanvasGroup(false);
            isTalking = false;

            return;
        }
    }

    private IEnumerator TextVisible()
    {
        dialogueTextBox.ForceMeshUpdate();
        int totalVisibleCharacters = dialogueTextBox.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            dialogueTextBox.maxVisibleCharacters = visibleCount;
            
            if (visibleCount % 3 == 0)
            {
                if (dialogueCounter % 2 == 0) // panda is talking
                {
                    audioSource.pitch = pandaPitchValue;
                }
                else
                {
                    audioSource.pitch = owlPitchValue;
                }
                audioSource.PlayOneShot(dialogueSpeakClip);
            }

            if (visibleCount >= totalVisibleCharacters)
            {
                //sentenceEnded = true;
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }

    private void HighlightSpeaker(string speakerName)
    {
        // Resset both image positions before any other changes
        owlDialogueObject.transform.position = oldOwlImagePosition;
        pandaDialogueObject.transform.position = oldPandaImagePosition;

        UnityEngine.UI.Image owlImage = owlDialogueObject.transform.GetComponent<UnityEngine.UI.Image>();
        UnityEngine.UI.Image pandaImage = pandaDialogueObject.transform.GetComponent<UnityEngine.UI.Image>();

        owlImage.color = tintedColor;
        pandaImage.color = tintedColor;

        if (speakerName == owlName)
        {
            owlDialogueObject.transform.position = new Vector3(oldOwlImagePosition.x, oldOwlImagePosition.y + 15,
                oldOwlImagePosition.z);
            owlImage.color = Color.white;
        }
        else if (speakerName == pandaName)
        {
            pandaDialogueObject.transform.position = new Vector3(oldPandaImagePosition.x, oldPandaImagePosition.y + 15,
                oldPandaImagePosition.z);
            pandaImage.color = Color.white;
        }
    }

    private void ShowCanvasGroup(bool value)
    {
        if (value)
        {
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1.0f;
        }
        else
        {
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player") { isNearby = true;  }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        isNearby = false;
    }
}

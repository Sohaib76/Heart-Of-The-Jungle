using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TalkToPlayer : MonoBehaviour
{
    public string interactKey = "e"; // The key to press for interaction (default is "e")
    public GameObject playerObject; // Reference to the player's GameObject
    private Collider2D playerCollider;

    // Hardcoded dialogue options
    private readonly Dictionary<int, string> beforeFindingScrollTalkWithOwl = new Dictionary<int, string>
    {
        {1, "Greetings, noble red panda. I am the keeper of ancient wisdom, and my scrolls have been stolen " +
            "by mischievous goblins.Will you embark on a quest to retrieve them and safeguard the " +
            "knowledge within?" },
        {2,  "I'm ready to help! What can you tell me about these goblins and the scrolls?"},
        {3,  "The goblins roam the depths of the jungle. Seek their lair and bring back the stolen " +
            "scrolls.The fate of our ancient knowledge rests upon your paws."},
    };
    private readonly Dictionary<int, string> afterFindingScrollTalkWithOwl = new Dictionary<int, string>
    {
        {1, "Ah, valiant red panda, you have returned! Do you possess the scrolls?" },
        {2,  "Yes, I've recovered them. The goblins won't trouble you any longer."},
        {3,  "You have my deepest gratitude. The jungle and its wisdom are indebted to your " +
            "bravery.May the jungle thrive with the knowledge you've preserved. Your courage will be " +
            "remembered in the leaves of our history."},
    };

    private string owlName = "Wise Owl";
    private string pandaName = "Rori the Panda";

    private int dialogueCounter = 0;
    private bool isTalking = false;

    private TextMeshProUGUI dialogueTextBox;
    private TextMeshProUGUI speakerTextBox;

    private CanvasGroup dialogueCanvas;

    private GameObject pandaDialogueObject;
    private GameObject owlDialogueObject;

    private Vector3 oldOwlImagePosition;
    private Vector3 oldPandaImagePosition;

    private Color tintedColor;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = playerObject.GetComponent<Collider2D>();

        // Find and disable the canvas objects
        dialogueCanvas = GameObject.Find("DialogueUI/DialogueCanvas").GetComponent<CanvasGroup>();

        // Find all the relevant child game objects
        dialogueTextBox = GameObject.Find("DialogueUI/DialogueCanvas/DialogueTextBox").GetComponent<TextMeshProUGUI>();
        speakerTextBox = GameObject.Find("DialogueUI/DialogueCanvas/SpeakerTextBox").GetComponent<TextMeshProUGUI>();

        pandaDialogueObject = GameObject.Find("DialogueUI/DialogueCanvas/DialogueTextBox/DialoguePandaImage");
        owlDialogueObject = GameObject.Find("DialogueUI/DialogueCanvas/DialogueTextBox/DialogueOwlImage");

        oldOwlImagePosition = owlDialogueObject.transform.position;
        oldPandaImagePosition = pandaDialogueObject.transform.position;

        tintedColor = pandaDialogueObject.transform.GetComponent<UnityEngine.UI.Image>().color;

        // Set up the appropriate initial data
        dialogueTextBox.text = beforeFindingScrollTalkWithOwl[dialogueCounter];
        speakerTextBox.text = owlName;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTalking && Input.GetKeyDown(interactKey))
        {
            if (playerCollider != null && playerObject != null)
            {
                isTalking = true;
                dialogueCanvas.interactable = true;
                dialogueCanvas.alpha = 1.0f;

                // Highlight the owl first
                HighlightSpeaker(owlName);
            }
        }

        if (isTalking)
        {
            if (Input.GetKeyDown(interactKey))
            {
                dialogueCounter++;

                if (dialogueCounter > beforeFindingScrollTalkWithOwl.Keys.Max())
                {
                    dialogueCounter = 0;

                    dialogueCanvas.interactable = false;
                    dialogueCanvas.alpha = 0f;
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

                dialogueTextBox.text = beforeFindingScrollTalkWithOwl[dialogueCounter];
            }
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
}

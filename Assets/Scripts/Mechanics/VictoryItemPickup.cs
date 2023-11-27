using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class VictoryItemPickup : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueCanvasPrefab;
    [SerializeField]
    private GameObject victoryModalPrefab;

    public string interactKey = "e"; // The key to press for interaction (default is "e")

    /**
     * Dialogue UI section
    **/
    // Hardcoded dialogue options
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
    private bool itemPickedUp = false;

    private TextMeshProUGUI dialogueTextBox;
    private TextMeshProUGUI speakerTextBox;

    private GameObject dialogueCanvas;
    private CanvasGroup dialogueCanvasGroup;

    private GameObject pandaDialogueObject;
    private GameObject owlDialogueObject;

    private Vector3 oldOwlImagePosition;
    private Vector3 oldPandaImagePosition;

    private Color tintedColor;

    /**
     * Victory Modal UI section
    **/
    private GameObject victoryModalObject;
    private CanvasGroup victoryModalCanvasGroup;


    // Start is called before the first frame update
    void Start()
    {
        // Instantiate a new dialogue canvas for the script to use
        dialogueCanvas = Instantiate(dialogueCanvasPrefab, Vector3.zero, Quaternion.identity);
        dialogueCanvas.transform.SetParent(GameObject.Find("DialogueUI").transform);

        // Instantiate a new victory modal canvas for the script to use
        victoryModalObject = Instantiate(victoryModalPrefab, Vector3.zero, Quaternion.identity);
        victoryModalObject.transform.SetParent(GameObject.Find("VictoryModalUI").transform);

        // Find and disable the canvas objects
        dialogueCanvasGroup = dialogueCanvas.GetComponent<CanvasGroup>();
        victoryModalCanvasGroup = victoryModalObject.GetComponent<CanvasGroup>();

        // Find all the relevant child game objects
        dialogueTextBox = dialogueCanvas.transform.Find("DialogueTextBox").GetComponent<TextMeshProUGUI>();
        speakerTextBox = dialogueCanvas.transform.Find("SpeakerTextBox").GetComponent <TextMeshProUGUI>();

        pandaDialogueObject = dialogueCanvas.transform.Find("DialogueTextBox/DialoguePandaImage").gameObject;
        owlDialogueObject = dialogueCanvas.transform.Find("DialogueTextBox/DialogueOwlImage").gameObject;

        oldOwlImagePosition = owlDialogueObject.transform.position;
        oldPandaImagePosition = pandaDialogueObject.transform.position;

        tintedColor = pandaDialogueObject.transform.GetComponent<UnityEngine.UI.Image>().color;

        // Set up the appropriate initial data
        dialogueTextBox.text = afterFindingScrollTalkWithOwl[1];
        speakerTextBox.text = owlName;
    }

    // Update is called once per frame
    void Update()
    {
        if (itemPickedUp)
        {
            // Start the dialogue
            if (!isTalking && Input.GetKeyDown(interactKey))
            {
                isTalking = true;

                // Hide the item picked up
                transform.GetComponent<SpriteRenderer>().enabled = false;

                ShowCanvasGroup(dialogueCanvasGroup, true);

                // Highlight the owl first
                HighlightSpeaker(owlName);

                return;
            }

            if (isTalking && Input.GetKeyDown(interactKey))
            {
                dialogueCounter++;

                // If dialogue is finished
                if (dialogueCounter > afterFindingScrollTalkWithOwl.Keys.Max())
                {
                    dialogueCounter = 0;

                    ShowCanvasGroup(dialogueCanvasGroup, false);
                    isTalking = false;

                    // Show the victory modal
                    itemPickedUp = false;
                    ShowCanvasGroup(victoryModalCanvasGroup, true);

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

                dialogueTextBox.text = afterFindingScrollTalkWithOwl[dialogueCounter];
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

    private void ShowCanvasGroup(CanvasGroup cg, bool value)
    {
        if (value)
        {
            cg.interactable = true;
            cg.alpha = 1.0f;
        }
        else
        {
            cg.interactable = false;
            cg.alpha = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player") 
        { 
            itemPickedUp = true;
        }
    }
}

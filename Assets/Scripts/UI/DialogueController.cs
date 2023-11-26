using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
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

    public string interactKey = "e";
    private int dialogueCounter = 1;
    public bool dialogueFinished = false;

    private TextMeshProUGUI dialogueTextBox;
    private TextMeshProUGUI speakerTextBox;

    private Transform pandaTransform;
    private Transform owlTransform;

    private Vector3 oldOwlImagePosition;
    private Vector3 oldPandaImagePosition;

    private Color tintedColor;

    private void Start()
    {
        // Find all the relevant child game objects
        dialogueTextBox = transform.Find("DialogueTextBox").GetComponent<TextMeshProUGUI>();
        speakerTextBox = transform.Find("SpeakerTextBox").GetComponent<TextMeshProUGUI>();

        pandaTransform = transform.Find("DialoguePandaImage");
        owlTransform = transform.Find("DialogueOwlImage");

        // Set up the appropriate initial data
        dialogueTextBox.text = beforeFindingScrollTalkWithOwl[dialogueCounter];
        speakerTextBox.text = owlName;

        oldOwlImagePosition = owlTransform.position;
        oldPandaImagePosition = pandaTransform.position;

        tintedColor = owlTransform.GetComponent<UnityEngine.UI.Image>().color;

        //Highlight the owl
        HighlightSpeaker(owlName);

    }

    private void HighlightSpeaker(string speakerName)
    {
        // Resset both image positions before any other changes
        owlTransform.position = oldOwlImagePosition;
        pandaTransform.position = oldPandaImagePosition;

        UnityEngine.UI.Image owlImage = owlTransform.GetComponent<UnityEngine.UI.Image>();
        UnityEngine.UI.Image pandaImage = pandaTransform.GetComponent<UnityEngine.UI.Image>();

        owlImage.color = tintedColor;
        pandaImage.color = tintedColor;

        if (speakerName == owlName)
        {
            owlTransform.position = new Vector3(oldOwlImagePosition.x, oldOwlImagePosition.y + 15,
                oldOwlImagePosition.z);
            owlImage.color = Color.white;
        }
        else if (speakerName == pandaName) 
        {
            pandaTransform.position = new Vector3(oldPandaImagePosition.x, oldPandaImagePosition.y + 15,
                oldPandaImagePosition.z);
            pandaImage.color = Color.white;
        }
    }

    public bool DialogueFinished()
    {
        return dialogueFinished;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            dialogueCounter++;

            if (dialogueCounter > beforeFindingScrollTalkWithOwl.Keys.Max())
            {
                dialogueFinished = true;
                dialogueCounter = 1;
                return;
            }

            if (dialogueCounter % 2 == 0) // Panda is talking
            {
                HighlightSpeaker(pandaName);
                speakerTextBox.text = pandaName;
            } else // Owl is talking
            {
                HighlightSpeaker(owlName);
                speakerTextBox.text = owlName;
            }

            dialogueTextBox.text = beforeFindingScrollTalkWithOwl[dialogueCounter];
        }

    }
}

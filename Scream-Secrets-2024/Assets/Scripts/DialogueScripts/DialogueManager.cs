using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Private Fields
    bool isTalking = false;

    [SerializeField] float distance; 
    float curResponseTracker = 0;

    [SerializeField] GameObject player;

    [Header("Key Inputs")]
    [SerializeField] KeyCode acceptDialogue = KeyCode.Return;
    [SerializeField] KeyCode startEndDialogue = KeyCode.E;

    // Public Fields
    public NPC npc;

    public GameObject dialogueUI;

    public TMP_Text npcName;
    public TMP_Text npcDialogueBox;
    public TMP_Text playerResponse;

    public float distanceToTrigger = 500f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueUI.SetActive(false);
    }

    void OnMouseOver()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance <= distanceToTrigger)
        {
            // Player Dialogue Selection
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                curResponseTracker++;
                if (curResponseTracker >= npc.playerDialogue.Length - 1)
                {
                    curResponseTracker = npc.playerDialogue.Length - 1;
                }
            }
            else if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                curResponseTracker--;
                if (curResponseTracker < 0)
                {
                    curResponseTracker = 0;
                }
            }

            // Trigger Dialogue
            if (Input.GetKeyDown(startEndDialogue) && isTalking == false)
            {
                StartConversation();
            }
            else if (Input.GetKeyDown(startEndDialogue) && isTalking == true)
            {
                EndDialogue();
            }

            // Series of responses
            if (curResponseTracker == 0 && npc.playerDialogue.Length >= 0)
            {
                playerResponse.text = npc.playerDialogue[0];
                if (Input.GetKeyDown(acceptDialogue))
                {
                    npcDialogueBox.text = npc.dialogue[1];
                }
            }
            else if(curResponseTracker == 1 && npc.playerDialogue.Length >= 1)
            {
                playerResponse.text = npc.playerDialogue[1];
                if (Input.GetKeyDown(acceptDialogue))
                {
                    npcDialogueBox.text = npc.dialogue[2];
                }
            }
            else if (curResponseTracker == 2 && npc.playerDialogue.Length >= 2)
            {
                playerResponse.text = npc.playerDialogue[2];
                if (Input.GetKeyDown(acceptDialogue))
                {
                    npcDialogueBox.text = npc.dialogue[3];
                }
            }
            // Increase more else if, if need more dialogue.
        }
    }

    void StartConversation()
    {
        isTalking = true;
        curResponseTracker = 0;
        dialogueUI.SetActive(true);
        npcName.text = npc.name;
        npcDialogueBox.text = npc.dialogue[0];
    }

    void EndDialogue()
    {
        isTalking = false;
        dialogueUI.SetActive(false);
    }
}

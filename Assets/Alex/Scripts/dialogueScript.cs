using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Collections;

[Serializable]

public struct DialoguePiece
{
    public string name;
    [TextArea] public string dialogue;
    public Sprite icon;

}

public class dialogueScript : MonoBehaviour
{
    //Dialogue Variables

    private dialogue currentDialogue;

    public TMPro.TMP_Text dialogueText;
    public TMPro.TMP_Text dialogueName;
    public Image dialogueIcon;

    private float textSpeed = 0.05f;
    private bool dialogueBoxOpen = false;
    private int dialogueIndex;
    private bool isDialogueRunning;

    private void Start()
    {

    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dialogueBoxOpen && !isDialogueRunning)
        {
            dialogueIndex++;
            if (dialogueIndex >= currentDialogue.lines.Count)
            {
                StopDialogue();
                return;
            }
            StartCoroutine(WriteDialogue(currentDialogue.lines[dialogueIndex]));
        }


        if (Input.GetKeyDown(KeyCode.Escape) && dialogueBoxOpen)
        {
            StopDialogue();
        }


    }


    public void StartDialogue(dialogue newDialogue)
    {
        currentDialogue = newDialogue;
        dialogueIndex = 0;



        dialogueBoxOpen = true;
        gameObject.SetActive(true);
        StartCoroutine(WriteDialogue(currentDialogue.lines[0]));

        //dialogueIcon.sprite = 
    }


    public void StopDialogue()
    {
        dialogueBoxOpen = false;
        gameObject.SetActive(false);
    }

    public IEnumerator WriteDialogue(DialoguePiece dialogue)
    {

        dialogueName.SetText(dialogue.name);
        dialogueText.SetText("");
        dialogueIcon.sprite = dialogue.icon;

        isDialogueRunning = true;
        for (int i = 0; i < dialogue.dialogue.Length; i++)
        {
            dialogueText.text += dialogue.dialogue[i];
            yield return new WaitForSeconds(textSpeed);
        }
        isDialogueRunning = false;

        //dialogueText.SetText(dialogue.dialogue);
        //dialogueIcon. = dialogue.icon;
    }

}

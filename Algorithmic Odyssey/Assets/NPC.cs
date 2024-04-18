using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    public TextMeshProUGUI NameContainer;
    public string NPCName;
    private int index;

    public GameObject contButton;
    public GameObject yesButton;
    public GameObject noButton;
    public float wordSpeed;
    public bool playerIsClose;


    void Start()
    {
        dialogueText.text = "";
        NameContainer.text = NPCName;
    }

    public void StartDialogue()
    {
        if (playerIsClose)
        {
            dialoguePanel.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(Typing());
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && playerIsClose)
        {
            if(dialoguePanel.activeInHierarchy)
            {
                zeroText();
            
            }
            else
            {
                dialoguePanel.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(Typing());
            }
        }


        
        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }

        if (index == dialogue.Length - 1 && dialogueText.text == dialogue[index])
        {
            yesButton.SetActive(true);
            noButton.SetActive(true);
        }
        
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {

        contButton.SetActive(false);
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StopAllCoroutines();
            StartCoroutine(Typing());
        }

        else
        {
            zeroText();
            yesButton.SetActive(false);
            noButton.SetActive(false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            // dialoguePanel.SetActive(true);
            // StartCoroutine(Typing());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject notenoughPanel;
    public GameObject maxPanel;
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
        if (dialogueText != null)
        {
            dialogueText.text = "";
        }
        if (NameContainer != null)
        {
            NameContainer.text = NPCName;
        }
    }

    public void StartDialogue()
    {
        if (playerIsClose)
        {
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(true);
            }
            StopAllCoroutines();
            StartCoroutine(Typing());
            SetMailboxButtons();
        }
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && playerIsClose)
        {
            dialogueText.text = "";

            if (dialoguePanel != null && dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                if (dialoguePanel != null)
                {
                    dialoguePanel.SetActive(true);
                }
                StopAllCoroutines();
                StartCoroutine(Typing());
                SetMailboxButtons();
            }
        }

        if (contButton != null && dialogueText.text == dialogue[index])
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
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (notenoughPanel != null) notenoughPanel.SetActive(false);
        if (maxPanel != null) maxPanel.SetActive(false);
        if (yesButton != null) yesButton.SetActive(false);
        if (noButton != null) noButton.SetActive(false);
    }

    public void clear()
    {
        dialogueText.text = "";
        index = 0;
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (yesButton != null) yesButton.SetActive(false);
        if (noButton != null) noButton.SetActive(false);
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
        if (contButton != null)
        {
            contButton.SetActive(false);
        }

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
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject != null)
            {
                playerIsClose = false;
                zeroText();
            }
        }
    }


    private void SetMailboxButtons()
    {
        if (Regex.IsMatch(dialoguePanel.name, @"^MailboxPanel \d+$"))
        {
            if (yesButton != null) yesButton.SetActive(true);
            if (noButton != null) noButton.SetActive(true);
        }
        else
        {
            if (yesButton != null) yesButton.SetActive(false);
            if (noButton != null) noButton.SetActive(false);
        }
    }
}

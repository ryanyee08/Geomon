using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dialogueText;

    [SerializeField]
    GameObject advanceButton;

    [SerializeField]
    GameObject attackButtonGroup;

    public void DisplayDialogue(string text)
    {
        dialogueText.text = text;
    }

    public void DisplayAdvanceTurnButton()
    {
        advanceButton.SetActive(true);
    }

    public void HideAdvanceTurnButton()
    {
        advanceButton.SetActive(false);
    }

    public void DisplayAttackButtons()
    {
        attackButtonGroup.SetActive(true);
    }

    public void HideAttackButtons()
    {
        attackButtonGroup.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI dialogueText;

    [SerializeField]
    GameObject advanceButton;

    [SerializeField]
    GameObject attackButtonGroup;

    [SerializeField]
    TextMeshProUGUI AttackButton1;

    [SerializeField]
    TextMeshProUGUI AttackButton2;

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

    public void UpdateAttackButtonText(string Attack1, string Attack2)
    {
        AttackButton1.text = Attack1;
        AttackButton2.text = Attack2;
    }
}

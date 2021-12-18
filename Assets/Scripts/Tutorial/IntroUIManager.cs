using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntroUIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI instructionalText;
    
    [SerializeField]
    GameObject textInputWindow;

    // Configures and displays the Text Input window for user to input a string
    // TODO - IN the future it would be ideal to use these functions along with the blocking command feature:
    // https://yarnspinner.dev/docs/unity/working-with-commands/
    // I think doing it this way I can create the UX where when the name prompt appears you can't continue the dialogue until completing the prompt
    public void DisplayTextInputWindow(string promptText)
    {
        instructionalText.text = promptText;
        textInputWindow.SetActive(true);
    }

    public void HideDisplayTextInputWindow()
    {
        instructionalText.text = "blank";
        textInputWindow.SetActive(false);
    }
}

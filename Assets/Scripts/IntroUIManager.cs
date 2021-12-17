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

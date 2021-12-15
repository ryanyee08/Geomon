using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverWorldUIManager : MonoBehaviour
{
    public GameObject interactionWindow;

    [SerializeField]
    TextMeshProUGUI interactionWindowText;

    [SerializeField]
    TextMeshProUGUI interactionButtonText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Need to take in a string to display the object name to the player
    public void DisplayInteractionWindow(string objectName)
    {
        interactionWindowText.text = objectName;
        interactionWindow.SetActive(true);
    }

    public void HideInteractionWindow()
    {
        interactionWindow.SetActive(false);
    }

    public void UpdateInteractionButtonText(string text)
    {
        interactionButtonText.text = text;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Interactable objects are the parent class for the various objects in the game that the user can interact with
public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    string ObjectName = "Interactable Object";

    [SerializeField]
    string ButtonText = "DoSomething";

    OverWorldUIManager UIManager;

    [SerializeField]
    Button InteractButton;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        // Okay in general I have been overusing serializefield to connect things
        // Since I would be attach this script or its child to many objects will use find
        // However in the future all of this modifying the UI Logic should be moved to the U
        
        // First Find the UI Manager
        GameObject OverworldUIManager = GameObject.Find("OverworldUIManager");
        UIManager = OverworldUIManager.GetComponent<OverWorldUIManager>();

        // So basically I ran into problems here because I was trying to find an inactive object since the popup is by default inactive
        // A better solution would probably be to find way to move the addlistener logic to the UIManager
        // However this I think would require delegates and figuring out how to add a function from a different class

        // Next Find the button that we attach functionality to
        //GameObject InteractiveButton = GameObject.Find("InteractiveButton");
        //InteractButton = InteractiveButton.GetComponent<Button>();
    }

    // When you collide with an interactable object it will display a prompt with a name
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Started Touching " + ObjectName);
        
        UIManager.DisplayInteractionWindow(ObjectName);
        UIManager.UpdateInteractionButtonText(ButtonText);

        InteractButton.onClick.AddListener(AssignedButtonTask);
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Stopped Touching " + ObjectName);
        UIManager.HideInteractionWindow();

        InteractButton.onClick.RemoveListener(AssignedButtonTask);
    }

    // Assign Button Task defines the behavior of the button in the prompt
    // Child classes will override this function to allow for different behaviors
    // In the future I would like to find a way to fix it so that attaching a function to the button is handled solely by UI Manager
    public virtual void AssignedButtonTask()
    {
        Debug.Log("I don't do anything");
    }


}

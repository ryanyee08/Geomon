using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

// INHERITANCE
public class NPC : InteractableObject
{
    [SerializeField]
    YarnProgram scriptToLoad;

    [SerializeField]
    public DialogueRunner dialogueRunner;

    // Add Audio to the NPC from the editor
    // Audio is accessed through AudioManager which is triggered by the Yarn Script
    [SerializeField]
    public List<AudioClip> NPCDialogue = new List<AudioClip>();

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        dialogueRunner = GameObject.Find("DialogueRunner").GetComponent<DialogueRunner>();
    }

    // POLYMORPHISM
    public override void AssignedButtonTask()
    {
        base.AssignedButtonTask();
        Debug.Log("Talking Time");
        dialogueRunner.Clear();
        dialogueRunner.Add(scriptToLoad);
        dialogueRunner.StartDialogue("Start");
    }

    public AudioClip GetAudioClip(int indexPosition)
    {
        AudioClip AudioClipToReturn = NPCDialogue[indexPosition];

        return AudioClipToReturn;
    }
}

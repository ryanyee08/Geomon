using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPC : InteractableObject
{
    [SerializeField]
    YarnProgram scriptToLoad;

    [SerializeField]
    public DialogueRunner dialogueRunner;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        dialogueRunner = GameObject.Find("DialogueRunner").GetComponent<DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void AssignedButtonTask()
    {
        base.AssignedButtonTask();
        Debug.Log("Talking Time");
        dialogueRunner.Clear();
        dialogueRunner.Add(scriptToLoad);
        dialogueRunner.StartDialogue("Start");
    }
}

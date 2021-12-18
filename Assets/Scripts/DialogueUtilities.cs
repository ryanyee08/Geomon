using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueUtilities : MonoBehaviour
{
    InMemoryVariableStorage variableStorage;

    // Provides addititonal functionality for yarn dialogue
    // Todo in future - Consider linking all other services to this object/script so that all yarncommands are called from here
    //  For example playAudio functions have yarncommand attached to them but it might be cleaner to link them all here
    // Will simplify the process of writing complex audio and all Yarn commands can simply reference this object
    
    [YarnCommand("SetPlayerName")]
    public void setPlayerName()
    {
        // Note that playername should be a default variable inside the InMemmoryVariableStorage component
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
        variableStorage.SetValue("$playerName", GameManager.GameManagerInstance.PlayerName);
    }

    [YarnCommand("SetRivalName")]
    public void setRivalName()
    {
        // Note that rivalName should be a default variable inside the InMemoryVariableStorage component
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();
        variableStorage.SetValue("$rivalName", GameManager.GameManagerInstance.RivalName);
    }

}

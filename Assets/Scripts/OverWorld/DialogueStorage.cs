using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dialogue Manager will store all the voice over audio files for the scene in which it's present
// Dialogue Manager is simply a storage and access point, the audiomanager will still be the main point of accessing audio
// Dialogue Manager is made up of serialized lists since each line is likely only really used in a specific scene and doesn't need to be available everwhere
public class DialogueStorage : MonoBehaviour
{

    [SerializeField]
    public List<AudioClip> NPCDialogue = new List<AudioClip>();  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Doorways are special points that the player can use to traverse between different scenes
public class Doorway : InteractableObject
{
    [SerializeField]
    string SceneName;

    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void AssignedButtonTask()
    {
        SceneManager.LoadScene("ProfessorScrubLab");
    }
}

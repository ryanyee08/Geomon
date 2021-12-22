using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Doorways are special points that the player can use to traverse between different scenes
public class Doorway : InteractableObject
{
    [SerializeField]
    string SceneName;

    [SerializeField]
    bool exitsOverworld;

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
        // If doorway leads from the overworld to another scene then we need to save the current position to GameManager for when player leaves the building 
        if (exitsOverworld == true)
        {
            GameManager.GameManagerInstance.lastOverWorldPosition = transform.position;
            GameManager.GameManagerInstance.isPlayerInBuilding = true;
        }
        else
        {
            GameManager.GameManagerInstance.isPlayerInBuilding = false;
        }

        SceneManager.LoadScene(SceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Stores the player's last position when the player transitions between the overworld and buildings/new scenes
    public Vector3 lastOverWorldPosition = new Vector3(-12, 1, -5);

    // Notes player's current location
    // this is reference by XXX
    public bool isPlayerInBuilding = false;

    // Marks Progress through Initial Tutorials
    public bool isNewPlayerIntroCompleted = false;
    public bool isEuclidTownFirstDialgueViewed = false;

    public static GameManager GameManagerInstance;

    // ENCAPSULATION
    // Stores the Player's name
    private string playerName = "Crimson";
    public string PlayerName
    {
        get
        {
            return playerName;
        }
        set
        {
            // Prevent Name from being blank
            if (string.IsNullOrWhiteSpace(value))
            {
                playerName = "Crimson";
                Debug.Log("Player name was set to: " + PlayerName);
            }
            else
            {
                playerName = value;
                Debug.Log("Player name was set to: " + value);
            }
        }
    }

    // Stores the Rival's Name
    private string rivalName = "Azure";
    public string RivalName
    {
        get
        {
            return rivalName;
        }
        set
        {
            // Prevent Name from being blank
            if (string.IsNullOrWhiteSpace(value))

            {
                rivalName = "Azure";
                Debug.Log("Player name was set to: " + rivalName);
            }
            else
            {
                rivalName = value;
                Debug.Log("Player name was set to: " + value);
            }
        }
    }


    private void Awake()
    {
        //Implement a Singleton pattern - will prevent more than one instance from occuring
        if (GameManagerInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        GameManagerInstance = this;
        DontDestroyOnLoad(gameObject);
    }
}

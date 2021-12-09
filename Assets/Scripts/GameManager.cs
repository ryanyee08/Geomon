using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;

    public string playerName = "Red";
    public string rivalName = "Blue";

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

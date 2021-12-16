using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

// Tutorial Manager contains all the functions and informationed needed to run the new player tutorial.

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Starts a new game and begins the intro sequence
    public void StartNewGame()
    {
        SceneManager.LoadScene("NewPlayerIntro");
    }

    // This function is called at the end of the intro sequence and loads the player to the game
    [YarnCommand("StartOverWorld")]
    public void StartOverWorld()
    {
        SceneManager.LoadScene("EuclidTown");
    }

    [YarnCommand("StartFirstBattle")]
    public void StartFirstBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }
}

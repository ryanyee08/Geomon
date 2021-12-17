using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using TMPro;

// Tutorial Manager contains all the functions and informationed needed to run the new player tutorial.
// 

public class TutorialManager : MonoBehaviour
{
    bool isPlayernNameSaved = false;

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

    // This function is used during the initial intro in order to collect the player's name
    [YarnCommand("RequestName")]
    public void RequestName()
    {
        // Find the Intro UI Manager
        IntroUIManager IntroUIManager = GameObject.Find("Canvas").GetComponent <IntroUIManager>();
        
        // Check to see if we are requesting playername or rival name
        if (isPlayernNameSaved == false)
        {
            IntroUIManager.DisplayTextInputWindow("Your Name: ");
        } 
        else
        {
            IntroUIManager.DisplayTextInputWindow("Rival Name: ");
        }
    }

    [YarnCommand("SaveName")]
    public void SaveName()
    {
        // Save whatever text is in the input to the game manager
        if (isPlayernNameSaved == false)
        {
            GameManager.GameManagerInstance.playerName = GameObject.Find("NameInputField").GetComponent<TMP_InputField>().text;
            isPlayernNameSaved = true;
        }
        else
        {
            GameManager.GameManagerInstance.rivalName = GameObject.Find("NameInputField").GetComponent<TMP_InputField>().text;
        }
        

        // Find the Intro UI Manager
        IntroUIManager IntroUIManager = GameObject.Find("Canvas").GetComponent<IntroUIManager>();

        // Set the Window to inactive
        IntroUIManager.HideDisplayTextInputWindow();
    }

    // This function is called at the end of the intro sequence and loads the player to the game
    [YarnCommand("StartOverWorld")]
    public void StartOverWorld()
    {
        SceneManager.LoadScene("EuclidTown");
    }

    // This function is called to start the player's first Geomon Battle
    [YarnCommand("StartFirstBattle")]
    public void StartFirstBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }
}

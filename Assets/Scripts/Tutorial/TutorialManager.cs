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

    [SerializeField]
    GameObject Rival;

    [SerializeField]
    YarnProgram EuclidTownIntroScript;

    public void Start()
    {
        // Find the Dialgoue Runner and load it with the Euclid Town Intro if the player hasn't seen it yet
        if (GameManager.GameManagerInstance.isEuclidTownFirstDialgueViewed == false && GameManager.GameManagerInstance.isNewPlayerIntroCompleted == true)
        {
            Debug.Log("Displaying Euclid Town Tutorial");
            DialogueRunner dialogueRunner = GameObject.Find("DialogueRunner").GetComponent<DialogueRunner>();
            dialogueRunner.Add(EuclidTownIntroScript);
            dialogueRunner.StartDialogue("EuclidTownIntro");
        }

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
            GameManager.GameManagerInstance.PlayerName = GameObject.Find("NameInputField").GetComponent<TMP_InputField>().text;
            isPlayernNameSaved = true;
        }
        else
        {
            GameManager.GameManagerInstance.RivalName = GameObject.Find("NameInputField").GetComponent<TMP_InputField>().text;
        }

        // Clear the Input Field
        GameObject.Find("NameInputField").GetComponent<TMP_InputField>().text = "";

        // Find the Intro UI Manager
        IntroUIManager IntroUIManager = GameObject.Find("Canvas").GetComponent<IntroUIManager>();

        // Set the Window to inactive
        IntroUIManager.HideDisplayTextInputWindow();
    }

    // This function is called at the end of the intro sequence and loads the player to the game
    // Todo in future this is a good candidate to move to the Yarn Utility script
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

    // This function spawns a rival
    [YarnCommand("SpawnRival")]
    public void SpawnRival()
    {
        Vector3 RivalSpawn = GameObject.Find("RivalSpawnPoint").transform.position;
        Instantiate(Rival, RivalSpawn, Quaternion.identity);

    }

    [YarnCommand("DespawnRival")]
    public void DespawnRival()
    {
        GameObject Rival = GameObject.Find("Rival(Clone)");
        Destroy(Rival);
    }

    [YarnCommand("MarkIntroComplete")]
    public void MarkIntroComplete()
    {
        GameManager.GameManagerInstance.isNewPlayerIntroCompleted = true;
        Debug.Log("The Player has completed Prof Shrubs New Player Intro");
    }

    [YarnCommand("MarkEuclidTownIntroComplete")]
    public void MarkEuclidTownIntroComplete()
    {
        GameManager.GameManagerInstance.isEuclidTownFirstDialgueViewed = true;
        Debug.Log("The Player has viewed the first dialgoue in Euclid Town");
    }
} 

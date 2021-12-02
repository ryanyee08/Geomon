using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    // UI Stuff
    [SerializeField]
    DialogueManager dialogueManager;

    public Button continueButton;

    // Data from GameManager
    // This is a bit overkill since there is only one fight in the battle
    // However code will be cleaner if i can just reference the local variable
    // Also if project were to be expanded there will be different opponents
    // later on Gamemanager will store the name of npc that started the fight or wild geomon
    [SerializeField]
    string opponentName = GameManager.GameManagerInstance.rivalName;
    [SerializeField]
    string playerName = GameManager.GameManagerInstance.playerName;

    // Management of Battle
    [SerializeField]
    bool isPlayerTurn;

    // For experimenting with refining turn flow
    public int yourGeomonHP = 4;
    public int opponentGeomonHP = 3;



    enum BattlePhase
    {
        StartBattle,
        DecideTurnOrder,
        SelectGeomon, // Will not be used until having more than one geomon is supported
        StartTurn,
        GetPlayerAction, // Will not be used until switching is implemented
        GetAttack,
        DeclareAttack,
        DeclareDamage,
        Endturn,
        EndBattle
    }

    [SerializeField]
    BattlePhase lastBattlePhase;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.AddListener(AdvanceBattle);

        StartBattle();
    }

    void AdvanceBattle()
    {
        if (lastBattlePhase == BattlePhase.StartBattle)
        {
            DecideTurnOrder();
        } 
        else if (lastBattlePhase == BattlePhase.DecideTurnOrder)
        {
            SelectGeomon();
        }
        else if (lastBattlePhase == BattlePhase.SelectGeomon)
        {
            StartTurn();
        }
        else if (lastBattlePhase == BattlePhase.StartTurn)
        {
            // If its the opponents turn, opponent will randomly pick a move
            if (isPlayerTurn == true)
            {
                GetAttack();
            }
            else
            {
                DeclareAttack();
            }
        }
        else if (lastBattlePhase == BattlePhase.GetAttack)
        {
            DeclareAttack();
        }
        else if (lastBattlePhase == BattlePhase.DeclareAttack)
        {
            DeclareDamage();
        }
        else if (lastBattlePhase == BattlePhase.DeclareDamage)
        {
            EndTurn();
        }
        else if (lastBattlePhase == BattlePhase.EndBattle)
        {
            Debug.Log("Battle is Over");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    // Battle Start - Displaying the initial prompt to the player
    void StartBattle()
    {
        Debug.Log(opponentName + " wants to fight");
        dialogueManager.DisplayDialogue(opponentName + " wants to fight");
        lastBattlePhase = BattlePhase.StartBattle;
    }

    void DecideTurnOrder()
    {
        string firstTurnPlayerName;
        var n = Random.Range(0, 2);
        if (n == 0)
        {
            Debug.Log(playerName + " goes first");
            firstTurnPlayerName = playerName;
            isPlayerTurn = true;
        }
        else
        {
            Debug.Log(opponentName + " goes first");
            firstTurnPlayerName = opponentName;
            isPlayerTurn = false;

        }

        dialogueManager.DisplayDialogue(firstTurnPlayerName + " goes first");
        lastBattlePhase = BattlePhase.DecideTurnOrder;
    }

    void SelectGeomon ()
    {
        Debug.Log("Choose your Geomon");
        dialogueManager.DisplayDialogue("Choose your Geomon");
        lastBattlePhase = BattlePhase.SelectGeomon;
    }

    void StartTurn ()
    {
        string currentTurnPlayer;
        if (isPlayerTurn == true)
        {
            currentTurnPlayer = playerName;
            Debug.Log("<Player's> Turn");
        } else
        {
            currentTurnPlayer = opponentName;
            Debug.Log("<Opponent's> Turn");
        }

        dialogueManager.DisplayDialogue("It's " + currentTurnPlayer + "'s Turn");
        lastBattlePhase = BattlePhase.StartTurn;
    }

    void GetAttack()
    {
        Debug.Log("What attack will Geomon use?");
        dialogueManager.DisplayDialogue("What attack will Geomon use?");
        lastBattlePhase = BattlePhase.GetAttack;
    }

    void DeclareAttack()
    {
        Debug.Log("Geomon used trignometry");
        dialogueManager.DisplayDialogue("Geomon used trignometry");
        lastBattlePhase = BattlePhase.DeclareAttack;
    }
    void DeclareDamage()
    {
        if (isPlayerTurn == true)
        {
            opponentGeomonHP = opponentGeomonHP - 1;
            dialogueManager.DisplayDialogue("Opposing Geomon took 1 damage!");
            Debug.Log("Opposing Geomon took 1 damage!");
            Debug.Log("opponent Geomon has " + opponentGeomonHP + " Hp Remaining");
        } 
        else
        {
            yourGeomonHP = yourGeomonHP - 1;
            dialogueManager.DisplayDialogue("Your Geomon took 1 damage!");
            Debug.Log("Your Geomon took 1 damage!");
            Debug.Log(yourGeomonHP + " Hp Remaining");
        }
        
        lastBattlePhase = BattlePhase.DeclareDamage;
    }

    void EndTurn()
    {
        CheckVictory();
    }

    // Checks to see if the player has won by eliminating the opponents Geomon
    void CheckVictory()
    {
        // If victory conditions are met then advance game to the end game flow
        Debug.Log("Checking if victory conditions were met");
        if (opponentGeomonHP == 0)
        {
            EndBattle();
            lastBattlePhase = BattlePhase.EndBattle;
        } 
        // Otherwise start a new turn
        else
        {
            Debug.Log("Victory Conditions not met...continuing game");
            
            // Turn goes to the other player
            isPlayerTurn = !isPlayerTurn;
            string upcomingTurnPlayerName;

            if (isPlayerTurn == true)
            {
                Debug.Log(playerName + " goes next");
                upcomingTurnPlayerName = playerName;
            } else
            {
                Debug.Log(opponentName + " goes next");
                upcomingTurnPlayerName = opponentName;
            }

            dialogueManager.DisplayDialogue("Now its " + upcomingTurnPlayerName + "'s turn");
            Debug.Log("Now its the other player's turn");

            lastBattlePhase = BattlePhase.StartTurn;
        }
    }

    void EndBattle()
    {
        dialogueManager.DisplayDialogue("You have won the battle!");
        Debug.Log("You have won the battle!");
    }

}

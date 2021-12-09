using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BattleManager is responsible for the flow of the battle and determining victory conditions
// It will take in input by the player and output the results of the player's decision through dialogueManager
// It will eventually read the data from YourGeomon and OpponentGeomon objects
public class BattleManager : MonoBehaviour
{
    // Other Objects that need to be referenced
    [SerializeField]
    AttackDatabase attackDatabase;
    
    // UI Stuff
    [SerializeField]
    DialogueManager dialogueManager;
    [SerializeField]
    BattleUIManager battleUIManager;

    public Button continueButton;
    public Button attackButton1;
    public Button attackButton2;

    // Data from GameManager (todo)
    [SerializeField]
    string opponentName = "Blue";
    [SerializeField]
    string playerName = "Red";

    // Management of Battle
    [SerializeField]
    bool isPlayerTurn;

    // For experimenting with Geomon Objects

    Geomon yourGeomon = new Geomon("Cubis", 4, "Tackle", "CubeBlast");
    Geomon opponentGeomon = new Geomon("Sphero", 3, "Tackle", "SphereBlast");

    enum BattlePhase
    {
        StartBattle,
        DecideTurnOrder,
        SelectGeomon, // Will not be used until switching is implemented
        StartTurn,
        //Upkeep, //This would be used to handle things that need to resolve at start of turn
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
        // Link up advance battle button
        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.AddListener(AdvanceBattle);

        // TODO Link up the attack buttons
            // Code does here
            // more code
            // such code

        // Update the battle Displays
        battleUIManager.UpdateYourGeomonNameDisplay(yourGeomon.geomonName);
        battleUIManager.UpdateOpponentGeomonNameDisplay(opponentGeomon.geomonName);
        battleUIManager.UpdateYourGeomonLevelDisplay(yourGeomon.level);
        battleUIManager.UpdateOpponentGeomonLevelDisplay(opponentGeomon.level);
        battleUIManager.UpdateYourGeomonHpDisplay(yourGeomon.currentHP, yourGeomon.maximumHP);
        battleUIManager.UpdateOpponentGeomonHPDisplay(opponentGeomon.currentHP, opponentGeomon.maximumHP);

        StartBattle();
    }

    void AdvanceBattle()
    {
        if (lastBattlePhase == BattlePhase.StartBattle)
        {
            DecideTurnOrder();
        } 
        //else if (lastBattlePhase == BattlePhase.DecideTurnOrder)
        //{
        //    SelectGeomon();
        //}
        // THe ine below would would be selectGeomon but its being skipped for now
        else if (lastBattlePhase == BattlePhase.DecideTurnOrder)
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

    // *** Start of Main Battle Phase Functions *** //

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

    // Displays the attack buttons to take in user selection of attacks
    void GetAttack()
    {
        Debug.Log("What attack will Geomon use?");
        dialogueManager.DisplayDialogue("What attack will Geomon use?");

    }

    // Announces what attack the player has selected TODO plays the animation for the attack
    void DeclareAttack()
    {
        Debug.Log("Geomon used trignometry");
        dialogueManager.DisplayDialogue("Geomon used trignometry");
        lastBattlePhase = BattlePhase.DeclareAttack;
    }

    void DeclareDamage()
    {
        // Debug stuff
        checkAllGeomon();

        if (isPlayerTurn == true)
        {
            opponentGeomon.TakeDamage(1);
            battleUIManager.UpdateOpponentGeomonHPDisplay(opponentGeomon.currentHP, opponentGeomon.maximumHP);
            dialogueManager.DisplayDialogue("Opposing Geomon took 1 damage!");
            Debug.Log("Opposing Geomon took 1 damage!");
        } 
        else
        {
            yourGeomon.TakeDamage(1);
            battleUIManager.UpdateYourGeomonHpDisplay(yourGeomon.currentHP, yourGeomon.maximumHP);
            dialogueManager.DisplayDialogue("Your Geomon took 1 damage!");
            Debug.Log("Your Geomon took 1 damage!");
        }

        // Debug stuff
        checkAllGeomon();

        lastBattlePhase = BattlePhase.DeclareDamage;
    }

    void EndTurn()
    {
        CheckVictory();
    }

    // *** End of Main Battle Phase Functions *** //

    // *** Start of Battle Helper Functions *** //

    // Executes the effects of an attack
    void SelectAttack()
    {

        // Advance to the next state
        lastBattlePhase = BattlePhase.GetAttack;
        // Hide the attack buttons and redisplay the advance button
    }

    // Checks to see if the player has won by eliminating the opponents Geomon
    void CheckVictory()
    {
        // If victory conditions are met then advance game to the end game flow
        Debug.Log("Checking if victory conditions were met");
        if (opponentGeomon.currentHP == 0)
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

    // *** End of Battle Helper Functions *** //

    // ** Everything below this line is for debug functions

    // Displays current stats for Your/Opponent Geomon
    void checkAllGeomon()
    {
        // Displays your Geomon Data
        Debug.Log("Your Geomon Name: " + yourGeomon.geomonName + "   Current HP: " + yourGeomon.currentHP +  "   Max HP: " + yourGeomon.maximumHP + "   hasFainted: " + yourGeomon.hasFainted);
        Debug.Log("Opponent Geomon Name: " + opponentGeomon.geomonName + "   Current HP: " + opponentGeomon.currentHP + "   Max HP: " + opponentGeomon.maximumHP + "   hasFainted: " + opponentGeomon.hasFainted);
    }

}

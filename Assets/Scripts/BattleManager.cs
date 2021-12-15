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
    [SerializeField]
    AudioManager audioManager;
    
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

    Geomon yourGeomon = new Geomon("Cubis", 8, "Tackle", "CubeBlast");
    Geomon opponentGeomon = new Geomon("Sphero", 6, "Tackle", "SphereBlast");

    [SerializeField]
    string yourActiveGeomonName;
    [SerializeField]
    string opponentActiveGeomonName;

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

    // In a future refactor I'm pretty sure I can just use one variable to track since there is only ever one attack per turn
    [SerializeField]
    string selectedPlayerAttackName;

    [SerializeField]
    string selectedOpponentAttackName;

    // Start is called before the first frame update
    void Start()
    {
        // This is temporary and should be refactored into Geomon Manager
        yourActiveGeomonName = yourGeomon.geomonName;
        opponentActiveGeomonName = opponentGeomon.geomonName;

        // Link up advance battle button
        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.AddListener(AdvanceBattle);

        // Link up attack 1 button
        Button attk1btn = attackButton1.GetComponent<Button>();
        attk1btn.onClick.AddListener(() => SelectAttack(1));

        // Link up attack 2 button
        Button attk2btn = attackButton2.GetComponent<Button>();
        attk2btn.onClick.AddListener(() => SelectAttack(2));

        // Update the battle Displays
        battleUIManager.UpdateYourGeomonNameDisplay(yourGeomon.geomonName);
        battleUIManager.UpdateOpponentGeomonNameDisplay(opponentGeomon.geomonName);
        battleUIManager.UpdateYourGeomonLevelDisplay(yourGeomon.level);
        battleUIManager.UpdateOpponentGeomonLevelDisplay(opponentGeomon.level);
        battleUIManager.UpdateYourGeomonHpDisplay(yourGeomon.currentHP, yourGeomon.maximumHP);
        battleUIManager.UpdateOpponentGeomonHPDisplay(opponentGeomon.currentHP, opponentGeomon.maximumHP);

        // Update the Attack button Text 
        dialogueManager.UpdateAttackButtonText(attackDatabase.GetAttackName(yourGeomon.Attack1), attackDatabase.GetAttackName(yourGeomon.Attack2));

        StartBattle();
    }

    void AdvanceBattle()
    {
        // In the future I would rewrite this as a switch
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

        // Play Audio when the button is clicked
        audioManager.playInterfaceFx("fInterfaceInput");
    }

    // *** Start of Main Battle Phase Functions *** //

    // Battle Start - Displaying the initial prompt to the player
    void StartBattle()
    {
        string StartbattleText = opponentName + " wants to fight!";

        dialogueManager.DisplayDialogue(StartbattleText);
        Debug.Log(StartbattleText);

        lastBattlePhase = BattlePhase.StartBattle;
    }

    void DecideTurnOrder()
    {
        string firstTurnPlayerName;
        var n = Random.Range(0, 2);
        if (n == 0)
        {
            firstTurnPlayerName = playerName;
            isPlayerTurn = true;
            Debug.Log(playerName + " get's to go first");
        }
        else
        {
            firstTurnPlayerName = opponentName;
            isPlayerTurn = false;
            Debug.Log(opponentName + " gets to go first");

        }

        dialogueManager.DisplayDialogue(firstTurnPlayerName + " gets to go first.");
        lastBattlePhase = BattlePhase.DecideTurnOrder;
    }

    void SelectGeomon ()
    {
        Debug.Log("Choose your Geomon.");
        dialogueManager.DisplayDialogue("Choose your Geomon.");
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

        dialogueManager.DisplayDialogue("It's " + currentTurnPlayer + "'s Turn.");
        lastBattlePhase = BattlePhase.StartTurn;
    }

    // Displays the attack buttons to take in user selection of attacks
    void GetAttack()
    {
        Debug.Log("What attack will " + yourActiveGeomonName + " use?");
        dialogueManager.DisplayDialogue("What attack will " + yourActiveGeomonName + " use?");
        dialogueManager.HideAdvanceTurnButton();
        dialogueManager.DisplayAttackButtons();
    }

    // Announces what attack the player has selected TODO plays the animation for the attack
    // In future I would refactor so that the display attack/play sound is a more generalized function
    void DeclareAttack()
    {
        if (isPlayerTurn == true)
        {
            // Lookup the name of the attack selected
            string attackname = attackDatabase.GetAttackName(selectedPlayerAttackName);

            // Display the attack to the player
            Debug.Log(yourActiveGeomonName + " used " + attackname);
            dialogueManager.DisplayDialogue(yourActiveGeomonName + " used " + attackname + "!");

            // Play Audio for the attack
            audioManager.playFx(attackDatabase.GetAttackSound(selectedPlayerAttackName));
        }
        else
        {
            // Select an attack for the opponent to use
            selectedOpponentAttackName = OpponentAttackSelection();
            string attackname = attackDatabase.GetAttackName(selectedOpponentAttackName);

            // Display the opponent's attack to the player
            Debug.Log(opponentName + "'s " + opponentActiveGeomonName + " used " + attackname);
            dialogueManager.DisplayDialogue(opponentName + "'s " + opponentActiveGeomonName + " used " + attackname + "!");

            // Play Audio for the attack
            audioManager.playFx(attackDatabase.GetAttackSound(selectedOpponentAttackName));
        }

        lastBattlePhase = BattlePhase.DeclareAttack;
    }

    void DeclareDamage()
    {
        // Debug stuff
        checkAllGeomon();

        if (isPlayerTurn == true)
        {
            // Lookup how much damange to do and then apply it to Opponent Geomon
            int attackDamage = attackDatabase.GetAttackDamage(selectedPlayerAttackName);
            opponentGeomon.TakeDamage(attackDamage);

            // Update Battle UI to reflect new HP and display message to player
            battleUIManager.UpdateOpponentGeomonHPDisplay(opponentGeomon.currentHP, opponentGeomon.maximumHP);
            dialogueManager.DisplayDialogue(opponentName + "'s " + opponentActiveGeomonName + " took " + attackDamage + " damage!");
            Debug.Log(opponentName + "'s " + opponentActiveGeomonName + " took " + attackDamage + " damage!");
        } 
        else
        {
            // Lookup how much damage your opponents attack will do to your Geomon and then apply it
            int attackDamage = attackDatabase.GetAttackDamage(selectedOpponentAttackName);
            yourGeomon.TakeDamage(attackDamage);

            // Update Battle UI to reflect new HP and display message to player
            battleUIManager.UpdateYourGeomonHpDisplay(yourGeomon.currentHP, yourGeomon.maximumHP);
            dialogueManager.DisplayDialogue("Your " + yourActiveGeomonName + " took " + attackDamage + " damage!");
            Debug.Log("Your " + yourActiveGeomonName + " took " + attackDamage + " damage!");
        }

        // Taking Damage Audio
        audioManager.playFx("fTakeDamage");

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
    public void SelectAttack(int attackNumber)
    {
        // Take in the value selected by player
        Debug.Log("Player Selected Attack Number: " + attackNumber);

        // Lookup the name of the attack
        switch (attackNumber)
        {
            case 1:
                selectedPlayerAttackName = yourGeomon.Attack1;
                break;
            case 2:
                selectedPlayerAttackName = yourGeomon.Attack2;
                break;
        }

        Debug.Log("Selection corresponds to: " + selectedPlayerAttackName);

        // Advance to the next state
        lastBattlePhase = BattlePhase.GetAttack;
        AdvanceBattle();
        // Hide the attack buttons and redisplay the advance button
        dialogueManager.HideAttackButtons();
        dialogueManager.DisplayAdvanceTurnButton();
    }

    // Used to select an attack for the opponent during their turn
    public string OpponentAttackSelection()
    {
        string attackname = "String";
       
        // Randomly pick a number between 1 and the total number of attacks which atm is hard limited at 2
        var n = Random.Range(1, 3);

        switch (n)
        {
            case 1:
                attackname = opponentGeomon.Attack1;
                break;
            case 2:
                attackname = opponentGeomon.Attack2;
                break;
        }
        return attackname;
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

            dialogueManager.DisplayDialogue("Now its " + upcomingTurnPlayerName + "'s turn!");
            Debug.Log("Now its the other player's turn");

            lastBattlePhase = BattlePhase.StartTurn;
        }
    }

    void EndBattle()
    {
        dialogueManager.DisplayDialogue("You have won the battle!");
        Debug.Log("You have won the battle!");

        // Play Audio when Victory Achieved
        audioManager.playMusic("mVictory");
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

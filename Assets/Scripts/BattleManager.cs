using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    int TempPoints = 3;
    [SerializeField]
    int BattleState = 0;
    
    public Button continueButton;
    public TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = continueButton.GetComponent<Button>();
        btn.onClick.AddListener(AdvanceBattle);

        StartBattle();
    }

    void AdvanceBattle()
    {
        if (BattleState == 1)
        {
            DecideTurnOrder();
        } 
        else if (BattleState == 2)
        {
            SelectGeomon();
        }
        else if (BattleState == 3)
        {
            StartTurn();
        }
        else if (BattleState == 4)
        {
            GetAttack();
        }
        else if (BattleState == 5)
        {
            DeclareAttack();
        }
        else if (BattleState == 6)
        {
            DeclareDamage();
        }
        else if (BattleState == 7)
        {
            EndTurn();
        }
        else if (BattleState == 8)
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
        Debug.Log(GameManager.GameManagerInstance.rivalName + " wants to fight");
        dialogueText.text = GameManager.GameManagerInstance.rivalName + " wants to fight";
        BattleState = 1;
    }

    void DecideTurnOrder()
    {
        var n = Random.Range(0, 2);
        Debug.Log(n + " goes first");
        dialogueText.text = n + " goes first";
        BattleState = 2;
    }

    void SelectGeomon ()
    {
        Debug.Log("Choose your Geomon");
        dialogueText.text = "Choose your Geomon";
        BattleState = 3;
    }

    void StartTurn ()
    {
        Debug.Log("It's <Player's> Turn");
        dialogueText.text = "It's <Player's> Turn";
        BattleState = 4;
    }

    void GetAttack()
    {
        Debug.Log("What attack will Geomon use?");
        dialogueText.text = "What attack will Geomon use?";
        BattleState = 5;
    }

    void DeclareAttack()
    {
        Debug.Log("Geomon used trignometry");
        dialogueText.text = "Geomon used trignometry";
        BattleState = 6;
    }
    void DeclareDamage()
    {
        TempPoints--;
        Debug.Log("Opposing Geomon took 1 damage!");
        dialogueText.text = "Opposing Geomon took 1 damage!";
        Debug.Log(TempPoints + " Hp Remaining");
        CheckVictory();
    }

    void EndTurn()
    {
        dialogueText.text = "Now its the other player's turn";
        Debug.Log("Now its the other player's turn");
        BattleState = 3;
    }

    // Checks to see if the player has won by eliminating the opponents Geomon
    void CheckVictory()
    {
        if (TempPoints == 0)
        {
            EndBattle();
            BattleState = 8;
        } else
        {
            BattleState = 7;
        }
    }

    void EndBattle()
    {
        dialogueText.text = "You have won the battle!";
        Debug.Log("You have won the battle!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attack object stores the various properties that define what should happen when executing an attack
public class Attack
{
    public string attackName;
    public int attackDamage;
    public string attackSound;

    public Attack(string mattackName, int mattackDamage, string mattackSound)
    {
        attackName = mattackName;
        attackDamage = mattackDamage;
        attackSound = mattackSound;
    }
}

// TODO - In the future the attack database should be static 

// Attack Database is basically a dictionary that contains every possible attack in the game
public class AttackDatabase : MonoBehaviour
{
    // Create the attack objects
    Attack Tackle = new Attack("Tackle", 1, "fAttackTackle");
    Attack CubeBlast = new Attack("Cube Blast", 2, "fAttackBlast");
    Attack SphereBlast = new Attack("Sphere Blast", 2, "fAttackBlast");

    // Create the attack Dictionary
    Dictionary<string, Attack> AttackDictionary = new Dictionary<string, Attack>();

    public void Awake()
    {
        // Add Attacks to dictionary
        AttackDictionary.Add("Tackle", Tackle);
        AttackDictionary.Add("CubeBlast", CubeBlast);
        AttackDictionary.Add("SphereBlast", SphereBlast);
    }

    public string GetAttackName(string attack)
    {
        string stringToReturn = AttackDictionary[attack].attackName;
        return stringToReturn;
    }

    public int GetAttackDamage(string attack)
    {
        int intToReturn = AttackDictionary[attack].attackDamage;
        return intToReturn;
    }

    public string GetAttackSound(string attack)
    {
        string stringToReturn = AttackDictionary[attack].attackSound;
        return stringToReturn;
    }

}

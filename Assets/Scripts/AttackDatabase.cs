using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attack object stores the various properties that define what should happen when executing an attack
public class Attack
{
    public string attackName;
    public int attackDamage;

    public Attack(string mattackName, int mattackDamage)
    {
        attackName = mattackName;
        attackDamage = mattackDamage;
    }
}

// Attack Database is basically a dictionary that contains every possible attack in the game
public class AttackDatabase : MonoBehaviour
{
    // Create the attack objects
    Attack Tackle = new Attack("Tackle", 1);
    Attack CubeBlast = new Attack("Cube Blast", 2);
    Attack SphereBlast = new Attack("Sphere Blast", 2);

    // Create the attack Dictionary
    Dictionary<string, Attack> AttackDictionary = new Dictionary<string, Attack>();

    public void Start()
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



}

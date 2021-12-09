using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geomon
{
    public string geomonName;
    public int level;
    public int currentHP;
    public int maximumHP;
    public bool hasFainted;
    public string Attack1;
    public string Attack2;

    public Geomon (string mgeomonName, int mmaximumHP, string mAttack1, string mAttack2) {
        geomonName = mgeomonName;
        level = 5;
        currentHP = mmaximumHP;
        maximumHP = mmaximumHP;
        hasFainted = false;
        Attack1 = mAttack1;
        Attack2 = mAttack2;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHP = currentHP - damageAmount;
        if (currentHP <= 0)
        {
            currentHP = 0;
            hasFainted = true;
        }
    }

}

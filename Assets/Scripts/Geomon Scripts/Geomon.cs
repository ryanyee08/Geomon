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

    public Geomon (string mgeomonName, int mmaximumHP) {
        geomonName = mgeomonName;
        level = 5;
        currentHP = mmaximumHP;
        maximumHP = mmaximumHP;
        hasFainted = false;
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

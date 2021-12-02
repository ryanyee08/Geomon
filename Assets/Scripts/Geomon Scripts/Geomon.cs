using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geomon : MonoBehaviour
{
    public string geomonName;
    public int currentHP;
    public int maximumHP;
    public int level;
    public bool hasFainted = false;

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

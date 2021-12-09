using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    // Your Battle Window
    [SerializeField]
    TextMeshProUGUI yourGeomonNameText;
    [SerializeField]
    TextMeshProUGUI yourGeomonHPText;
    [SerializeField]
    TextMeshProUGUI yourGeomonLevel;

    // Opponent Battle Window
    [SerializeField]
    TextMeshProUGUI opponentGeomonNameText;
    [SerializeField]
    TextMeshProUGUI opponentGeomonHPText;
    [SerializeField]
    TextMeshProUGUI opponentGeomonLevel;

    public void UpdateYourGeomonNameDisplay(string GeomonName)
    {
        yourGeomonNameText.text = GeomonName;
    }

    public void UpdateOpponentGeomonNameDisplay(string GeomonName)
    {
        opponentGeomonNameText.text = GeomonName;
    }

    public void UpdateYourGeomonLevelDisplay(int level)
    {
        string levelstring = "LVL: " + level.ToString();
        yourGeomonLevel.text = levelstring;
    }

    public void UpdateOpponentGeomonLevelDisplay(int level)
    {
        string levelstring = "LVL: " + level.ToString();
        opponentGeomonLevel.text = levelstring;
    }

    public void UpdateYourGeomonHpDisplay(int currentHP, int maxHP)
    {
        string HPDisplay = "HP: " + currentHP + "/" + maxHP;
        yourGeomonHPText.text = HPDisplay;
    }

    public void UpdateOpponentGeomonHPDisplay(int currentHP, int maxHP)
    {
        string HPDisplay = "HP: " + currentHP + "/" + maxHP;
        opponentGeomonHPText.text = HPDisplay;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

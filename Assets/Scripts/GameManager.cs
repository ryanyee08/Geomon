using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GameManagerInstance;

    [SerializeField]
    public string playerName;
    [SerializeField]
    public string rivalName;

    private void Awake()
    {
        //Implement a Singleton pattern - will prevent more than one instance from occuring
        if (GameManagerInstance != null)
        {
            Destroy(gameObject);
            return;
        }
        GameManagerInstance = this;
        DontDestroyOnLoad(gameObject);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int currentLevel { get; private set; } = 1;
    
    void Start()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(this);
        } 
        else if (instance != this) 
        {
            Destroy(gameObject);
        }
    }
}

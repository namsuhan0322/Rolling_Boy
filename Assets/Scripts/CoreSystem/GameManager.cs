using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int currentLevel { get; private set; } = 1;
    public float bpm = 120f; // BPM 기본값

    public float GetMovementSpeed(float distancePerBeat = 5f)
    {
        float beatInterval = 60f / bpm; // 1비트당 시간 (초)
        float speed = distancePerBeat / beatInterval; // 이동 속도 계산
        return speed;
    }

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
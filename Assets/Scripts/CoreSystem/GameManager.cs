using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int currentLevel = 1;
    public float bpm = 120f; // BPM 기본값
    public float beatInterval; //UiManager2로가 가져가기 위해서
    public bool isCheckPoint = false;
    
    public GameObject player;

    public float GetMovementSpeed(float distancePerBeat = 5f) 
    {
        beatInterval = 60f / bpm; // 1비트당 시간 (초)
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

    void Update()
    {
        if (GameStateManager.instance != null)
        {
            if (Input.GetMouseButtonDown(0) && !GameStateManager.instance.isGameRunning)
            {
                StartCoroutine(GameStateManager.instance.StartGameWithDelay(1f));
            }
        }
    }
}
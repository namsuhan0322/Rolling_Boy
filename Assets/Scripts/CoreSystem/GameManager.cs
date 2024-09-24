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

    void Update()
    {
        // 게임이 실행 중이지 않고, 마우스 왼쪽 버튼 클릭 시
        if (Input.GetMouseButtonDown(0) && !GameStateManager.instance.isGameRunning)
        {
            // 3초 후 게임을 시작하는 코루틴 호출
            StartCoroutine(GameStateManager.instance.StartGameWithDelay(3f));
        }
    }
}
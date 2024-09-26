using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int currentLevel { get; private set; } = 1;
    public float bpm = 120f; // BPM 기본값

    private Vector3 lastCheckpointPosition;
    private bool checkpointSet = false;
    
    public GameObject player;
    public GameObject restartFromCheckpointButton; // 체크포인트에서 다시하기 버튼
    public GameObject restartFromBeginningButton;  // 처음부터 다시하기 버튼

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
        if (Input.GetMouseButtonDown(0) && !GameStateManager.instance.isGameRunning)
        {
            StartCoroutine(GameStateManager.instance.StartGameWithDelay(3f));
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        player.SetActive(true);

        if (checkpointSet) 
        {
            Vector3 respawnPosition = lastCheckpointPosition;
            respawnPosition.y = Mathf.Max(respawnPosition.y + 1f, 0f); // Y 좌표를 최소 0으로 설정
            player.transform.position = respawnPosition;
        }
        else 
        {
            player.transform.position = new Vector3(2f, 0.3218206f, 0.6643624f);
        }

        restartFromCheckpointButton.SetActive(false);
        restartFromBeginningButton.SetActive(false);
    
        player.GetComponent<PlayerController>().StartInvulnerability(); // 무적 상태 시작
        
        // LevelCreator 인스턴스를 찾고 리셋 호출
        LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
        if (levelCreator != null)
        {
            levelCreator.ResetMap(); // 맵 초기화
        }
    }
    
    // 플레이어가 죽었을 때 UI 버튼을 표시하는 함수
    public void ShowRestartButtons()
    {
        if (checkpointSet)
        {
            restartFromCheckpointButton.SetActive(true); // 체크포인트에서 다시하기 버튼 활성화
            restartFromBeginningButton.SetActive(false);
        }
        else
        {
            restartFromCheckpointButton.SetActive(false); // 체크포인트가 없으면 비활성화
            restartFromBeginningButton.SetActive(true);
        }
    }

    // 처음부터 다시 시작하는 버튼 클릭 시
    public void RestartFromBeginning()
    {
        checkpointSet = false; // 체크포인트 초기화
    
        if (player != null)
        {
            RespawnPlayer(player); // 플레이어 리스폰
        }
        else
        {
            Debug.LogError("Player 오브젝트가 설정되지 않았습니다.");
        }
    }


    // 체크포인트에서 다시 시작하는 버튼 클릭 시
    public void RestartFromCheckpoint()
    {
        if (checkpointSet)
        {
            RespawnPlayer(player); // 체크포인트에서 리스폰
        }
        else
        {
            Debug.LogError("체크포인트가 설정되지 않았습니다.");
        }
    }
    
    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpointPosition = checkpointPosition;
        checkpointSet = true;
    }
}
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

    //private Vector3 lastCheckpointPosition;
    //private bool checkpointSet = false;
    
    public GameObject player;
    public GameObject restartFromCheckpointButton; // 체크포인트에서 다시하기 버튼
    public GameObject restartFromBeginningButton;  // 처음부터 다시하기 버튼
    
    public GameObject nextStageButton; // 버튼을 연결할 GameObject
    public GameObject returnToMainMenuButton; // 메인 메뉴로 돌아가는 버튼

    public float GetMovementSpeed(float distancePerBeat = 5f) //5였음
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
        if (Input.GetMouseButtonDown(0) && !GameStateManager.instance.isGameRunning)
        {
            StartCoroutine(GameStateManager.instance.StartGameWithDelay(3f));
        }
    }

    /*public void RespawnPlayer(GameObject player)
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
            // 초기 스폰 위치 사용
            player.transform.position = new Vector3(2f, 0.3218206f, 0.6643624f);
        }


        restartFromCheckpointButton.SetActive(false);  
        restartFromBeginningButton.SetActive(false);

        player.GetComponent<PlayerController>().StartInvulnerability(); // 무적 상태 시작

        // LevelCreator 초기화 및 맵 로드
        LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
        if (levelCreator != null)
        {
            levelCreator.ResetMap(); // 맵 초기화
            levelCreator.LoadMap();  // 맵 다시 로드
        }
    }
    
    // 플레이어가 죽었을 때 UI 버튼을 표시하는 함수
    public void ShowRestartButtons()
    {
        if (checkpointSet)
        {
            restartFromCheckpointButton.SetActive(true);
            restartFromBeginningButton.SetActive(false);
        }
        else
        {
            restartFromCheckpointButton.SetActive(false);
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
    
    public void OnFinishReached()
    {
        // 게임이 끝났음을 알리고 버튼을 활성화
        if (currentLevel == 3)
        {

            returnToMainMenuButton.SetActive(true);
            restartFromCheckpointButton.SetActive(false);
            restartFromBeginningButton.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            nextStageButton.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ProceedToNextStage()
    {
        nextStageButton.SetActive(false);

        // 마지막 스테이지가 Level3일 때 (이 부분은 예시)
        if (currentLevel == 3)
        {
            OnFinishReached();
        }
        else
        {
            currentLevel++; // 스테이지 증가
            Debug.Log("Proceeding to next stage. Current level: " + currentLevel);

            checkpointSet = false; // 체크포인트 초기화

            RespawnPlayer(player); // 플레이어 리스폰

            LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
            if (levelCreator != null)
            {
                levelCreator.ResetMap(); // 기존 맵 초기화
                levelCreator.LoadMap();  // 새로운 맵 로드
            }

            StartCoroutine(GameStateManager.instance.StartGameWithDelay(3f)); // 클릭 후 게임 시작
        }
    }

    // 메인 화면으로 돌아가는 버튼 클릭 시
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // 게임 속도를 다시 정상화
        SceneManager.LoadScene("MainScene"); // 메인 메뉴로 이동
    }*/
}
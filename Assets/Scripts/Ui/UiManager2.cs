using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UiManager2 : MonoBehaviour
{
    [SerializeField]
    GameObject[] gameUI2 = new GameObject[6];
    private GameObject player;

    [SerializeField]
    private Slider progressSlider;
    [SerializeField]
    private TextMeshProUGUI progress_Text;
    [SerializeField]
    private Slider progressSlider_2;
    [SerializeField]
    private TextMeshProUGUI progress_Text_2;
    [SerializeField]
    private TextMeshProUGUI realTimeProgress;
    [SerializeField]
    private TextMeshProUGUI diaMond_N_Text;
    [SerializeField]
    private TextMeshProUGUI diaMond_N_Text_2;


    LevelCreator levelCreator;
    private int allBlock = 1;
    private float currentBlock = 0f;

    private float point;
    private float bestScore;
    private int diaMond_N = 0;  //다이아몬드 개수를 넣을 변수
    private int allDiaMond_N = 10;

    public static bool isStartSetting = false;

    //옮긴 코드--------------------------------------------
    private static Vector3 lastCheckpointPosition;
    private static bool checkpointSet = false;
    //------------------------------------------------
    public static UiManager2 Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        levelCreator = FindObjectOfType<LevelCreator>();
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        RespawnPlayer(player); // 플레이어 리스폰

        //UI초기화
        gameUI2[0].SetActive(true);
        for (int i = 1; i < gameUI2.Length; i++)
        {
            gameUI2[i].SetActive(false);
        }
        //최고점수 저장
        bestScore = PlayerPrefs.GetFloat("BestScore");

    }

    void Update()
    {
        StartGame(); 
        CheckScore();
        CheckClearGame();

        if (gameUI2[3].activeSelf == true)
        {
            gameUI2[2].SetActive(false);
            Time.timeScale = 0f;          //PausePlay()에 넣었을 때, 시간정지후 UI화면이 안뜨는 오류발생해서 씀.
        }
    }

    public void CheckClearGame() //성공, 실패여부를 체크하는 메서드
    {
        if (player.activeSelf == false)
        {
            GameOver();
        }

        if (player.transform.position.z >= allBlock)  //게임 클리어조건
        {
            GameOver();
        }
    }

    public void CheckScore()    //실시간으로 점수 체크하는 메서드
    {
        allBlock = levelCreator.allBlock ;
        allBlock = Mathf.CeilToInt(allBlock * GameManager.instance.beatInterval);
        currentBlock = player.transform.position.z + 1;
        point = currentBlock / allBlock;

        float processPoint = Mathf.Ceil(Mathf.Min(point * 100, 100f));
        realTimeProgress.text = string.Format("{0}%", processPoint.ToString());     //실시간 게임진행도표시
    }

    //버튼메서드 , 게임완료 메서드

    public void StartGame() //게임시작 UI설정
    {
        if (GameStateManager.instance.isGameRunning)
        {
            gameUI2[0].SetActive(false);
            gameUI2[1].SetActive(true);
            gameUI2[2].SetActive(true);
        }
    }

    public void UseSheild()
    {

    }

    public void PausePlay() //게임 일시정지
    {
        gameUI2[3].SetActive(true);
    }

    public void ContinueGame() //게임 정지취소
    {
        gameUI2[3].SetActive(false);
        gameUI2[2].SetActive(true);
        Time.timeScale = 1f;
    }

    public void GameOver()  //게임완료메서드        BestScore, BestScore_st 정적변수 설정
    {
        if (point >= 1)
        {
            point = 1f;
        }

        if (bestScore < point)
        {
            PlayerPrefs.SetFloat("BestScore", point);
            PlayerPrefs.SetString("BestScore_st", string.Format("{0}%", Mathf.Ceil(point * 100).ToString()));
            PlayerPrefs.Save();
        }

        if (player.activeSelf == false)  //게임 클리어시 플레이어의 중력값을 0으로 해서 판단.
        {
            gameUI2[2].SetActive(false);
            gameUI2[4].SetActive(true);

            progressSlider.value = point;
            progress_Text.text = string.Format("{0}%", Mathf.Ceil(point * 100).ToString());
            diaMond_N_Text.text = string.Format("{0}/{1}",diaMond_N ,allDiaMond_N);
        }
        else
        {
            gameUI2[2].SetActive(false);
            gameUI2[5].SetActive(true);

            progressSlider_2.value = point;
            progress_Text_2.text = string.Format("{0}%", Mathf.Ceil(point * 100).ToString());
            diaMond_N_Text_2.text = string.Format("{0}/{1}", diaMond_N, allDiaMond_N);
        }
    }
//------------------------------------------------------------------------------------------------------------옮겨온 코드

    public void RespawnPlayer(GameObject player)
    {

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

        player.GetComponent<PlayerController>().StartInvulnerability(); // 무적 상태 시작

        // LevelCreator 초기화 및 맵 로드
        LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
        if (levelCreator != null)
        {
            levelCreator.ResetMap(); // 맵 초기화
            levelCreator.LoadMap();  // 맵 다시 로드
        }
    }

    // 처음부터 다시 시작하는 버튼 클릭 시
    public void RestartBeginning()
    {
        checkpointSet = false; // 체크포인트 초기화
        
        if (player != null)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    public void RestartCheckPoint()
    {

        if (player != null)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        Debug.Log("된다");
        lastCheckpointPosition = checkpointPosition;
        checkpointSet = true;
    }
    
    public void OnFinishReached()
    {
        // 게임이 끝났음을 알리고 버튼을 활성화
        if (GameManager.instance.currentLevel == 3)
        {
            //기존 게임클리어UI에서 다음으로 가는 버튼 비활성화
            //다시하기 버튼 가운데로 이동 추가예정

            Time.timeScale = 0f;
        }
    }

    public void ProceedToNextStage()
    {
        Debug.Log("된다");
        int currentLevel = GameManager.instance.currentLevel;
        // 마지막 스테이지가 Level3일 때 (이 부분은 예시)
        if (currentLevel == 3)
        {
            OnFinishReached();
        }
        else
        {

            currentLevel++; // 스테이지 증가
            Debug.Log("Proceeding to next stage. Current level: " + currentLevel);

            GameManager.instance.currentLevel = currentLevel;
            checkpointSet = false; // 체크포인트 초기화

            LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
            if (levelCreator != null)
            {
                levelCreator.ResetMap(); // 기존 맵 초기화
                levelCreator.LoadMap();  // 새로운 맵 로드
            }

            SceneManager.LoadScene("GameScene");
        }
    }

    // 메인 화면으로 돌아가는 버튼 클릭 시
    public void ReturnToMainMenu()
    {
        isStartSetting = true;

        Time.timeScale = 1f; // 게임 속도를 다시 정상화
        SceneManager.LoadScene("MainScene"); // 메인 메뉴로 이동
    }
}



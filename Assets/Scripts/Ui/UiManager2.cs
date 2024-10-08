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



    LevelCreator levelCreator;
    private int allBlock = 1;
    private float currentBlock = 0f;

    private float point;
    private float[] bestScore = new float[4];

    public static bool isStartSetting = false;

    //--------------------------------------------
    private static Vector3 lastCheckpointPosition;
    private static bool checkpointSet = false;
    //------------------------------------------------
    public static UiManager2 Instance;
    private bool isStarting = false;
    private bool isStarting_02 = false;
    private float allBlock_2;
    private float variable;

    private Rigidbody rb;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        levelCreator = FindObjectOfType<LevelCreator>();
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        RespawnPlayer(player);

        ClearActive();
        gameUI2[0].SetActive(true);
        isStarting = true;
        isStarting_02 = true;

        GameManager.instance.isUpdate = true;  //UIManager 의 업데이트활성화

        if (GameManager.instance.currentLevel == 1)
        {
            bestScore[0] = PlayerPrefs.GetFloat("BestScore1");
        }
        if (GameManager.instance.currentLevel == 2)
        {
            bestScore[1] = PlayerPrefs.GetFloat("BestScore2");
        }
        if (GameManager.instance.currentLevel == 3)
        {
            bestScore[2] = PlayerPrefs.GetFloat("BestScore3");
        }
        if (GameManager.instance.currentLevel == 4)
        {
            bestScore[3] = PlayerPrefs.GetFloat("BestScore4");
        }
    }
    void ClearActive()
    {
        for (int i = 0; i < gameUI2.Length; i++)
        {
            gameUI2[i].SetActive(false);
        }
    }

    void Update()
    {
        StartGame();
        CheckScore();
        CheckClearGame();

        if (gameUI2[3].activeSelf == true)
        {
            gameUI2[2].SetActive(false);
            Time.timeScale = 0f;          
        }

        if (GameManager.instance.currentLevel == 1)
        {
            if (bestScore[0] < point)
            {
                PlayerPrefs.SetFloat("BestScore1", point);
                PlayerPrefs.SetString("BestScore_st1", string.Format("{0}%", Mathf.Ceil(point * 100).ToString()));
                PlayerPrefs.Save();
            }
        }
        if (GameManager.instance.currentLevel == 2)
        {
            if (bestScore[1] < point)
            {
                PlayerPrefs.SetFloat("BestScore2", point);
                PlayerPrefs.SetString("BestScore_st2", string.Format("{0}%", Mathf.Ceil(point * 100).ToString()));
                PlayerPrefs.Save();
            }
        }
        if (GameManager.instance.currentLevel == 3)
        {
            if (bestScore[2] < point)
            {
                PlayerPrefs.SetFloat("BestScore3", point);
                PlayerPrefs.SetString("BestScore_st3", string.Format("{0}%", Mathf.Ceil(point * 100).ToString()));
                PlayerPrefs.Save();
            }
        }
        if (GameManager.instance.currentLevel == 4)
        {
            if (bestScore[3] < point)
            {
                PlayerPrefs.SetFloat("BestScore4", point);
                PlayerPrefs.SetString("BestScore_st4", string.Format("{0}%", Mathf.Ceil(point * 100).ToString()));
                PlayerPrefs.Save();
            }
        }
    }
    
    public bool IsCheckpointSet()
    {
        return checkpointSet;
    }

    public void CheckClearGame()
    {
        if (player.activeSelf == false)
        {
            GameOver();
        }

        if (player.transform.position.z >= allBlock_2 - 2)
        {
            GameOver();
            rb.constraints = RigidbodyConstraints.FreezePositionY;

        }
    }
    public void CheckScore()
    {
        if (isStarting)
        {
            variable = 60 / GameManager.instance.bpm;
            allBlock = levelCreator.allBlock;
            allBlock_2 = allBlock * variable;
            isStarting = false;
        }
        currentBlock = player.transform.position.z + 1;
        point = currentBlock / allBlock_2;

        float processPoint = Mathf.Ceil(Mathf.Min(point * 100, 100f));
        realTimeProgress.text = string.Format("{0}%", processPoint.ToString());
    }

    public void StartGame()
    {
        if (GameStateManager.instance.isGameRunning && isStarting_02)
        {
            gameUI2[0].SetActive(false);
            gameUI2[1].SetActive(true);
            gameUI2[2].SetActive(true);
            isStarting_02 = false;
        }
    }

    public void UseSheild()
    {

    }

    public void PausePlay() 
    {
        gameUI2[3].SetActive(true);
        SoundManager.instance.PauseSound("Game1");
    }

    public void ContinueGame()
    {
        gameUI2[3].SetActive(false);
        gameUI2[2].SetActive(true);
        Time.timeScale = 1f;
        
        SoundManager.instance.PlaySound("Game1");
    }

    public void GameOver()
    {
        if (point >= 1)
        {
            point = 1f;
        }

        if (player.activeSelf == false)
        {
            gameUI2[2].SetActive(false);
            gameUI2[4].SetActive(true);

            progressSlider.value = point;
            progress_Text.text = string.Format("{0}%", Mathf.Ceil(point * 100).ToString());
        }
        else
        {
            if (GameManager.instance.currentLevel != 4)
            {
                gameUI2[2].SetActive(false);
                gameUI2[5].SetActive(true);
            }

            progressSlider_2.value = point;
            progress_Text_2.text = string.Format("{0}%", Mathf.Ceil(point * 100).ToString());
        }
    }
//------------------------------------------------------------------------------------------------------------
    public void RespawnPlayer(GameObject player)
    {
        if (checkpointSet)
        {
            Vector3 respawnPosition = lastCheckpointPosition;
            respawnPosition.y = Mathf.Max(respawnPosition.y + 1f, 0f);
            player.transform.position = respawnPosition;
        }
        else 
        {
            player.transform.position = new Vector3(2f, 0.3218206f, 0.6643624f);
        }

        player.GetComponent<PlayerController>().StartInvulnerability();

        LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
        if (levelCreator != null)
        {
            levelCreator.ResetMap();
            levelCreator.LoadMap();
        }
    }
    
    public void RestartBeginning()
    {
        checkpointSet = false;
        
        if (player != null)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    
    public void RestartCheckPoint()
    {
        if (checkpointSet)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.Log("체크포인트가 할당되지 않았습니다");
        }
    }
    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpointPosition = checkpointPosition;
        checkpointSet = true;
    }
    
    public void OnFinishReached()
    {
        if (GameManager.instance.currentLevel == 4)
        {
            gameUI2[2].SetActive(false);
            gameUI2[6].SetActive(true);
            //Time.timeScale = 0f;
        }
    }

    public void ProceedToNextStage()
    {
        int currentLevel = GameManager.instance.currentLevel;
        if (currentLevel == 4)
        {
            OnFinishReached();
        }
        else
        {
            currentLevel++;
            Debug.Log("Proceeding to next stage. Current level: " + currentLevel);

            GameManager.instance.currentLevel = currentLevel;
            checkpointSet = false;

            LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
            if (levelCreator != null)
            {
                levelCreator.ResetMap();
                levelCreator.LoadMap();
            }

            SceneManager.LoadScene("GameScene");
        }
    }

    public void ReturnToMainMenu()
    {
        isStartSetting = true;

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}



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
    private int diaMond_N = 0;  //���̾Ƹ�� ������ ���� ����
    private int allDiaMond_N = 10;

    public static bool isStartSetting = false;

    //�ű� �ڵ�--------------------------------------------
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
        RespawnPlayer(player); // �÷��̾� ������

        //UI�ʱ�ȭ
        gameUI2[0].SetActive(true);
        for (int i = 1; i < gameUI2.Length; i++)
        {
            gameUI2[i].SetActive(false);
        }
        //�ְ����� ����
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
            Time.timeScale = 0f;          //PausePlay()�� �־��� ��, �ð������� UIȭ���� �ȶߴ� �����߻��ؼ� ��.
        }
    }

    public void CheckClearGame() //����, ���п��θ� üũ�ϴ� �޼���
    {
        if (player.activeSelf == false)
        {
            GameOver();
        }

        if (player.transform.position.z >= allBlock)  //���� Ŭ��������
        {
            GameOver();
        }
    }

    public void CheckScore()    //�ǽð����� ���� üũ�ϴ� �޼���
    {
        allBlock = levelCreator.allBlock ;
        allBlock = Mathf.CeilToInt(allBlock * GameManager.instance.beatInterval);
        currentBlock = player.transform.position.z + 1;
        point = currentBlock / allBlock;

        float processPoint = Mathf.Ceil(Mathf.Min(point * 100, 100f));
        realTimeProgress.text = string.Format("{0}%", processPoint.ToString());     //�ǽð� �������൵ǥ��
    }

    //��ư�޼��� , ���ӿϷ� �޼���

    public void StartGame() //���ӽ��� UI����
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

    public void PausePlay() //���� �Ͻ�����
    {
        gameUI2[3].SetActive(true);
    }

    public void ContinueGame() //���� �������
    {
        gameUI2[3].SetActive(false);
        gameUI2[2].SetActive(true);
        Time.timeScale = 1f;
    }

    public void GameOver()  //���ӿϷ�޼���        BestScore, BestScore_st �������� ����
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

        if (player.activeSelf == false)  //���� Ŭ����� �÷��̾��� �߷°��� 0���� �ؼ� �Ǵ�.
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
//------------------------------------------------------------------------------------------------------------�Űܿ� �ڵ�

    public void RespawnPlayer(GameObject player)
    {

        if (checkpointSet)
        {
            Vector3 respawnPosition = lastCheckpointPosition;
            respawnPosition.y = Mathf.Max(respawnPosition.y + 1f, 0f); // Y ��ǥ�� �ּ� 0���� ����
            player.transform.position = respawnPosition;
        }
        else 
        {
            // �ʱ� ���� ��ġ ���
            player.transform.position = new Vector3(2f, 0.3218206f, 0.6643624f);
        }

        player.GetComponent<PlayerController>().StartInvulnerability(); // ���� ���� ����

        // LevelCreator �ʱ�ȭ �� �� �ε�
        LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
        if (levelCreator != null)
        {
            levelCreator.ResetMap(); // �� �ʱ�ȭ
            levelCreator.LoadMap();  // �� �ٽ� �ε�
        }
    }

    // ó������ �ٽ� �����ϴ� ��ư Ŭ�� ��
    public void RestartBeginning()
    {
        checkpointSet = false; // üũ����Ʈ �ʱ�ȭ
        
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
        Debug.Log("�ȴ�");
        lastCheckpointPosition = checkpointPosition;
        checkpointSet = true;
    }
    
    public void OnFinishReached()
    {
        // ������ �������� �˸��� ��ư�� Ȱ��ȭ
        if (GameManager.instance.currentLevel == 4)
        {
            //���� ����Ŭ����UI���� �������� ���� ��ư ��Ȱ��ȭ
            //�ٽ��ϱ� ��ư ����� �̵� �߰�����

            Time.timeScale = 0f;
        }
    }

    public void ProceedToNextStage()
    {
        Debug.Log("�ȴ�");
        int currentLevel = GameManager.instance.currentLevel;
        // ������ ���������� Level3�� �� (�� �κ��� ����)
        if (currentLevel == 4)
        {
            OnFinishReached();
        }
        else
        {
            currentLevel++; // �������� ����
            Debug.Log("Proceeding to next stage. Current level: " + currentLevel);

            GameManager.instance.currentLevel = currentLevel;
            checkpointSet = false; // üũ����Ʈ �ʱ�ȭ

            LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
            if (levelCreator != null)
            {
                levelCreator.ResetMap(); // ���� �� �ʱ�ȭ
                levelCreator.LoadMap();  // ���ο� �� �ε�
            }

            SceneManager.LoadScene("GameScene");
        }
    }

    // ���� ȭ������ ���ư��� ��ư Ŭ�� ��
    public void ReturnToMainMenu()
    {
        isStartSetting = true;

        Time.timeScale = 1f; // ���� �ӵ��� �ٽ� ����ȭ
        SceneManager.LoadScene("MainScene"); // ���� �޴��� �̵�
    }
}



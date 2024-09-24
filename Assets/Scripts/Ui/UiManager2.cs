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


    private void Awake()
    {
        levelCreator = FindObjectOfType<LevelCreator>();
        player = GameObject.Find("Player");
    }

    private void Start()
    {
        gameUI2[0].SetActive(true);
        for (int i = 1; i < gameUI2.Length; i++)
        {
            gameUI2[i].SetActive(false);
        }

        bestScore = PlayerPrefs.GetFloat("BestScore");
    }

    void Update()
    {
        StartGame();

        if (gameUI2[3].activeSelf == true)
        {
            gameUI2[2].SetActive(false);
            Time.timeScale = 0f;          //화면정지시 타임스케일 0
        }

        if (player.activeSelf == false)
        {
            GameOver();
        }

        allBlock = levelCreator.allBlock;
        currentBlock = player.transform.position.z + 1;
        point = currentBlock / allBlock;
        realTimeProgress.text = string.Format("{0}%", Mathf.Ceil(point * 100).ToString());       //실시간 게임진행도표시

        if (player.transform.position.z >= allBlock)  //게임 클리어조건
        {
            GameOver();
        }
    }


    //버튼에 붙일 메서드 + 게임종료 메서드
    public void StartGame()
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

    public void GoBackMain()
    {
        isStartSetting = true;

        SceneManager.LoadScene("MainScene");
    }

    public void PausePlay()
    {
        gameUI2[3].SetActive(true);
    }

    public void ContinueGame()
    {
        gameUI2[3].SetActive(false);
        gameUI2[2].SetActive(true);
        Time.timeScale = 1f;
    }

    public void GameOver()  //BestScore, BestScore_st 정적변수 설정
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

    public void ReStartGame()
    {
        SceneManager.LoadScene("Test_GameScene");
        GameStateManager.instance.ReadyGame();
    }

    public void PlayNextGame()
    {

    }
}

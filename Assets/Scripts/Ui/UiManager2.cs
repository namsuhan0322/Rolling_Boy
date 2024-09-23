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
    private Slider progressSlider1;
    [SerializeField]
    private TextMeshProUGUI progress_Text;

    LevelCreator levelCreator;
    private int allBlock = 1;
    private float currentBlock = 0f;

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
    }

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

    public void GameOver()
    {
        gameUI2[2].SetActive(false);
        gameUI2[4].SetActive(true);

        allBlock = levelCreator.allBlock;
        currentBlock = player.transform.position.z + 1;
        progressSlider.value = currentBlock / allBlock;

        progress_Text.text = string.Format("{0}%" ,Mathf.Ceil((currentBlock / allBlock) * 100).ToString());
    }

    public void GameClear()
    {
        gameUI2[2].SetActive(false);
        gameUI2[5].SetActive(true);
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

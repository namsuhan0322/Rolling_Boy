using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameUI = new List<GameObject>();

    static List<bool> setActive = new List<bool>();
    private bool isStartSetting = false;

    [SerializeField]
    private GameObject[] levelUI = new GameObject[4];

    [SerializeField]
    private TextMeshProUGUI progress_Text1;
    [SerializeField]
    private TextMeshProUGUI progress_Text2;
    [SerializeField]
    private TextMeshProUGUI progress_Text3;
    [SerializeField]
    private TextMeshProUGUI progress_Text4;


    private void Awake()
    {

        isStartSetting = UiManager2.isStartSetting;

        for(int i = 0; i < setActive.Count; i++)
        {
            gameUI[i].SetActive(setActive[i]);
        }
    }
    private void Start()
    {
        if (!isStartSetting)
        {
            ClearUI();
            gameUI[0].SetActive(true);
            gameUI[1].SetActive(true);
            isStartSetting = false;
        }
        progress_Text1.SetText(PlayerPrefs.GetString("BestScore_st1", "0%"));
        progress_Text2.SetText(PlayerPrefs.GetString("BestScore_st2", "0%"));
        progress_Text3.SetText(PlayerPrefs.GetString("BestScore_st3", "0%"));
        progress_Text4.SetText(PlayerPrefs.GetString("BestScore_st4", "0%"));

        //SetInfoToUI();
    }
    void Update()
    {

    }

    private void SetInfoToUI()
    {
        if (PlayerPrefs.HasKey("BestScore_str1"))
        {
            progress_Text1.SetText(PlayerPrefs.GetString("BestScore_st1"));
        }
        if (PlayerPrefs.HasKey("BestScore_str2"))
        {
            progress_Text2.SetText(PlayerPrefs.GetString("BestScore_st2"));
        }
        if (PlayerPrefs.HasKey("BestScore_str3"))
        {
            progress_Text3.SetText(PlayerPrefs.GetString("BestScore_st3"));
        }
        if (PlayerPrefs.HasKey("BestScore_str4"))
        {
            progress_Text4.SetText(PlayerPrefs.GetString("BestScore_st4"));
        }

    }

    //--------------------------------------------------------------------------------------------------------------------------------
    private void ClearUI()  //UI초기화
    {
        for (int i = 1; i < gameUI.Count; i++)
        {
            gameUI[i].SetActive(false);
        }
    }
    // 기본화면에서 활성화
    public void HomeUI()
    {
        ClearUI();
        gameUI[0].SetActive(true);
        gameUI[1].SetActive(true);
    }
    public void LevelUI()
    {
        ClearUI();
        gameUI[0].SetActive(true);
        gameUI[2].SetActive(true);
    }
    public void SettingUI()
    {
        ClearUI();
        gameUI[0].SetActive(true);
        gameUI[3].SetActive(true);
    }
    //게임씬 전으로 이동
    public void ReadyUI_1()
    {
        ClearUI();
        gameUI[0].SetActive(false);
        gameUI[4].SetActive(true);
    }
    public void ReadyUI_2()
    {
        ClearUI();
        gameUI[0].SetActive(false);
        gameUI[5].SetActive(true);
    }

    //게임씬으로 이동
    public void PlayGame()
    {
        for (int i = 0; i < gameUI.Count; i++)
        {
            setActive.Add(gameUI[i].activeSelf);
            Debug.Log(setActive[i]);
        }

        LoadingSceneController.LoadScene("GameScene");
    }
}

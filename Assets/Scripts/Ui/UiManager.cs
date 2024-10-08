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

    bool isOpen = false;
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
        gameUI[0].SetActive(true);
        gameUI[1].SetActive(false);
        gameUI[2].SetActive(false);

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
    // 기본화면에서 활성화
    public void HomeUI()
    {
        gameUI[1].SetActive(false);
        gameUI[2].SetActive(false);
        gameUI[0].SetActive(true);
    }
    public void SettingUI()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            gameUI[1].SetActive(true);
        }
        else
        {
            gameUI[1].SetActive(false);
        }

    }
    //게임씬 전으로 이동
    public void ReadyUI_1()
    {
        gameUI[0].SetActive(false);
        gameUI[1].SetActive(false);
        gameUI[2].SetActive(true);
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

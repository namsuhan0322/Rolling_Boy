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

    [SerializeField]
    private TextMeshProUGUI[] settingUI = new TextMeshProUGUI[2];

    [SerializeField]
    private TextMeshProUGUI progress_Text1;
    [SerializeField]
    private TextMeshProUGUI progress_Text2;
    [SerializeField]
    private TextMeshProUGUI progress_Text3;
    [SerializeField]
    private TextMeshProUGUI progress_Text4;

    bool isOpen = false;
    bool isMusic = true;
    bool isInGameSound = true;

    private void Awake()
    {
        gameUI[0].SetActive(true);
        gameUI[1].SetActive(false);
        gameUI[2].SetActive(false);

        if (setActive.Count > 0)
        {
            for (int i = 0; i < gameUI.Count; i++)
            {
                gameUI[i].SetActive(setActive[i]);
            }
        }
    }
    private void Start()
    {
        
        progress_Text1.SetText(PlayerPrefs.GetString("BestScore_st1", "0%"));
        progress_Text2.SetText(PlayerPrefs.GetString("BestScore_st2", "0%"));
        progress_Text3.SetText(PlayerPrefs.GetString("BestScore_st3", "0%"));
        progress_Text4.SetText(PlayerPrefs.GetString("BestScore_st4", "0%"));
        
    }

    public void SelectMap_1()
    {
        GameManager.instance.currentLevel = 1;
    }

    public void SelectMap_2()
    {
        GameManager.instance.currentLevel = 2;
    }

    public void SelectMap_3()
    {
        GameManager.instance.currentLevel = 3;
    }

    public void SelectMap_4()
    {
        GameManager.instance.currentLevel = 4;
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
    public void MusicSetting()
    {
        isMusic = !isMusic;
        if (isMusic)
        {
            settingUI[0].SetText("음악소리 : 켜기");
        }
        else
        {
            settingUI[0].SetText("음악소리 : 끄기");
        }
    }

    public void InGameSoundSetting()
    {
        isInGameSound = !isInGameSound;

        if (isInGameSound)
        {
            settingUI[1].SetText("인게임사운드 : 켜기");
            SoundManager.instance.isSoundOff = false;
        }
        else
        {
            settingUI[1].SetText("인게임사운드 : 끄기");
            SoundManager.instance.isSoundOff = true;
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
        setActive.Clear();
        for (int i = 0; i < gameUI.Count; i++)
        {
            setActive.Add(gameUI[i].activeSelf);
            Debug.Log(setActive[i]);
        }

        LoadingSceneController.LoadScene("GameScene");
    }
}

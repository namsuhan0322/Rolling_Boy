using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> gameUI = new List<GameObject>();

    static List<bool> setActive = new List<bool>();
    private bool isStartSetting = false;

    //public static UiManager Instance;

    private void Awake()
    {
        /*if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/

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
    }
    void Update()
    {

    }

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
    /*public void HalloweenUI()
    {
        ClearUI();
        gameUI[0].SetActive(true);
        gameUI[3].SetActive(true);
    }*/
    public void StoreUI()
    {
        ClearUI();
        gameUI[0].SetActive(true);
        gameUI[3].SetActive(true);
    }
    public void SettingUI()
    {
        ClearUI();
        gameUI[0].SetActive(true);
        gameUI[4].SetActive(true);
    }
    //게임씬 전으로 이동
    public void ReadyUI_1()
    {
        ClearUI();
        gameUI[0].SetActive(false);
        gameUI[5].SetActive(true);
    }
    public void ReadyUI_2()
    {
        ClearUI();
        gameUI[0].SetActive(false);
        gameUI[6].SetActive(true);
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

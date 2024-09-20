using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] gameUI = new GameObject[6];

    private static UiManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {

    }
    private void ClearUI()  //UI초기화
    {
        for (int i = 1; i < gameUI.Length - 1; i++)
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
    public void HalloweenUI()
    {
        ClearUI();
        gameUI[0].SetActive(true);
        gameUI[3].SetActive(true);
    }
    public void StoreUI()
    {
        ClearUI();
        gameUI[0].SetActive(true);
        gameUI[4].SetActive(true);
    }
    public void SettingUI()
    {
        ClearUI();
        gameUI[0].SetActive(true);
        gameUI[5].SetActive(true);
    }
    //게임씬 전으로 이동
    public void ReadyUI_1()
    {
        ClearUI();
        gameUI[0].SetActive(false);
        gameUI[6].SetActive(true);
    }
    public void ReadyUI_2()
    {
        ClearUI();
        gameUI[0].SetActive(false);
        gameUI[7].SetActive(true);
    }

    //게임씬으로 이동
    public void PlayGame()
    {
        LoadingSceneController.LoadScene("GameScene");
    }

}

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
    private void ClearUI()
    {
        for (int i = 1; i < gameUI.Length - 1; i++)
        {
            gameUI[i].SetActive(false);
        }
    }
    public void HomeUI()
    {
        ClearUI();
        gameUI[1].SetActive(true);
    }
    public void LevelUI()
    {
        ClearUI();
        gameUI[2].SetActive(true);
    }
    public void HalloweenUI()
    {
        ClearUI();
        gameUI[3].SetActive(true);
    }
    public void StoreUI()
    {
        ClearUI();
        gameUI[4].SetActive(true);
    }
    public void SettingUI()
    {
        ClearUI();
        gameUI[5].SetActive(true);
    }
    public void PlayGame()
    {
        LoadingSceneController.LoadScene("GameScene");
    }

}

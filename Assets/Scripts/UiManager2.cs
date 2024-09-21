using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager2 : MonoBehaviour
{
    [SerializeField]
    GameObject[] gameUI2 = new GameObject[6];

    void Start()
    {
        
    }

    void Update()
    {
        
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
        //게임매니저에서 시간정지 코드 작성

    }

    public void ContinueGame()
    {

    }

    public void ReStartGame()
    {

    }

    public void PlayNextGame()
    {

    }
}

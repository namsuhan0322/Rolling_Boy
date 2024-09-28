using System.Collections;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;
    public bool isGameRunning = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ReadyGame();
    }

    public void ReadyGame() //게임 다시하기를 할 경우, DontDistroyOnLoad로 Start 메서드가 작동하지 않아서 수정
    {
        // 게임이 시작되면 초기에는 멈춘 상태로 설정
        Time.timeScale = 0f;
        isGameRunning = false;

    }

    // 3초 후 게임을 시작하는 코루틴
    public IEnumerator StartGameWithDelay(float delay)
    {
        Debug.Log("Game will start in " + delay + " seconds...");
        yield return new WaitForSecondsRealtime(delay); // 실제 시간을 기준으로 3초 대기
        StartGame(); // 3초 후 게임 시작
    }

    public void StartGame()
    {
        isGameRunning = true;
        Time.timeScale = 1f; // 게임 시작 시 시간을 정상으로 돌림
        Debug.Log("Game started!");
    }
}
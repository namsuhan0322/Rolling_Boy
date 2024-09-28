using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager3 : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject player;

    private static Vector3 lastCheckpointPosition_2;
    private static bool checkpointSet_2 = false;
    void Start()
    {
        gameOverUI.SetActive(false);

        RespawnPlayer(player);
    }

    void Update()
    {
        if (!player.activeSelf)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void RespawnPlayer(GameObject player)
    {

        if (checkpointSet_2)
        {
            Vector3 respawnPosition = lastCheckpointPosition_2;
            respawnPosition.y = Mathf.Max(respawnPosition.y + 1f, 0f); // Y 좌표를 최소 0으로 설정
            player.transform.position = respawnPosition;
        }
        else
        {
            // 초기 스폰 위치 사용
            player.transform.position = new Vector3(2f, 0.3218206f, 0.6643624f);
        }

        player.GetComponent<PlayerController>().StartInvulnerability(); // 무적 상태 시작

        // LevelCreator 초기화 및 맵 로드
        LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
        if (levelCreator != null)
        {
            levelCreator.ResetMap(); // 맵 초기화
            levelCreator.LoadMap();  // 맵 다시 로드
        }
    }
    public void RestartBeginning()
    {
        checkpointSet_2 = false; // 체크포인트 초기화

        SceneManager.LoadScene("GameScene_TokyoMachine_Play");
    }
    public void RestartCheckPoint()
    {
        if (checkpointSet_2)
        {
            SceneManager.LoadScene("GameScene_TokyoMachine_Play");
        }
        else
        {
            Debug.Log("체크포인트 도달하지 못하거나 처음부터 다시 시작하셨습니다");
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpointPosition_2 = checkpointPosition;
        checkpointSet_2 = true;
    }
}

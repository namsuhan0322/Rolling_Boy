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
            respawnPosition.y = Mathf.Max(respawnPosition.y + 1f, 0f); // Y ��ǥ�� �ּ� 0���� ����
            player.transform.position = respawnPosition;
        }
        else
        {
            // �ʱ� ���� ��ġ ���
            player.transform.position = new Vector3(2f, 0.3218206f, 0.6643624f);
        }

        player.GetComponent<PlayerController>().StartInvulnerability(); // ���� ���� ����

        // LevelCreator �ʱ�ȭ �� �� �ε�
        LevelCreator levelCreator = FindObjectOfType<LevelCreator>();
        if (levelCreator != null)
        {
            levelCreator.ResetMap(); // �� �ʱ�ȭ
            levelCreator.LoadMap();  // �� �ٽ� �ε�
        }
    }
    public void RestartBeginning()
    {
        checkpointSet_2 = false; // üũ����Ʈ �ʱ�ȭ

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
            Debug.Log("üũ����Ʈ �������� ���ϰų� ó������ �ٽ� �����ϼ̽��ϴ�");
        }
    }

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpointPosition_2 = checkpointPosition;
        checkpointSet_2 = true;
    }
}

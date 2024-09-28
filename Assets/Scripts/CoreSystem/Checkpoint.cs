using System.Runtime.CompilerServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌 시
        {

            UiManager2.Instance.SetCheckpoint(transform.position); // 체크포인트 위치 저장
        }
    }

    //테스트용 코드
    /*private GameObject player;
    private GameObject checkPointUI;
    private FindCheckpoint findCheckPoint;
    private bool isChaged = false;
    private bool endProcess = true;
    private void Start()
    {
        player = GameObject.Find("Player");
        checkPointUI = GameObject.Find("CheckPointCanvas");
        findCheckPoint = player.GetComponent<FindCheckpoint>();
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        if (gameObject.transform.position.x - 1 < player.transform.position.x && gameObject.transform.position.x + 1 > player.transform.position.x)
        {
            UiManager2.Instance.SetCheckpoint(transform.position);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            player.gameObject.SetActive(false);
        }

        
        if (gameObject.transform.position.x - 50 < player.transform.position.x && findCheckPoint.isFinded)
        {
            checkPointUI.gameObject.transform.position = new Vector3(2, 0, findCheckPoint.findedCheckPoint[0].transform.position.x);
            endProcess = true;
            Debug.Log("ehls");
        }

        if (endProcess)
        {
            if (findCheckPoint.findedCheckPoint[0].transform.position.x < player.transform.position.x)
            {
                findCheckPoint.isReset = true;
                findCheckPoint.isFinded = false;
                endProcess = false;
                findCheckPoint.isFinded = false;
            }
        }

    }*/
}

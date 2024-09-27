using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌 시
        {
            UiManager2 uiManager2 = FindObjectOfType<UiManager2>();

            uiManager2.SetCheckpoint(transform.position); // 체크포인트 위치 저장
        }
    }
}

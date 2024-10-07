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
            SoundManager.instance.SaveMusicTime("Game1");
        }
    }

}

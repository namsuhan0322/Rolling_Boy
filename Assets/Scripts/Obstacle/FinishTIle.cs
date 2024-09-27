using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTIle : MonoBehaviour
{
    public bool isFinishTile = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isFinishTile && other.CompareTag("Player"))
        {
            GameManager.instance.OnFinishReached(); // 플레이어가 도착하면 호출
        }
    }
}

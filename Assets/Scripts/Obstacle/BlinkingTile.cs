using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingTile : MonoBehaviour
{
    // 블링크 주기 (초)
    [SerializeField] private float period = 3.0f;
    private float startTime;
    private Transform[] childTiles;

    private bool blink = true;
    
    void Start()
    {
        // 시작 시간을 현재 시간으로 설정
        startTime = Time.time;

        // 자식 객체들 가져오기
        childTiles = GetComponentsInChildren<Transform>();

        // 자식 객체들의 활성화 상태 설정
        for (int i = 1; i < childTiles.Length; i++) {
            childTiles[i].gameObject.SetActive(i % 2 != 0);
        }
    }
    
    void Update()
    {
        // 주기가 지나면 블링크 상태에 따라 활성화/비활성화 상태 전환
        if (Time.time - startTime >= period) 
        {
            if (blink)
            {
                for (int i = 1; i < childTiles.Length; i++)
                {
                    childTiles[i].gameObject.SetActive(!childTiles[i].gameObject.activeSelf); 
                }
            }
            // 시작 시간 갱신
            startTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 충돌하면 블링크 중지
        if (other.CompareTag("Player")) 
        {
            blink = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 플레이어가 충돌을 벗어나면 블링크 재개
        if (other.CompareTag("Player")) 
        {
            blink = true;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpForce = 7.65f;   // 점프 힘 (Y축 방향)
    private Vector3 iniScale;   // 초기 스케일 저장 변수
    
    void Start()
    {
        // 초기 스케일을 저장
        iniScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어와 충돌하고 애니메이션이 활성화되지 않은 경우
        if (other.CompareTag("Player")) 
        {
            // 플레이어에게 점프 힘을 적용
            other.attachedRigidbody.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            
            // 플레이어를 공중 상태로 설정
            other.gameObject.GetComponent<PlayerController>().airborne = true;
        }
    }
}

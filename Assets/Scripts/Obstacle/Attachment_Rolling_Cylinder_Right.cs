using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment_Rolling_Cylinder_Right : MonoBehaviour
{
    public float baseRange = 5f;       // 기본 감지 범위
    public float baseMoveSpeed = 2f;   // 기본 이동 속도
    public float moveDistance = 5f;     // 이동 거리

    public float minRange = 7f;
    public float maxRange = 11f;
    
    private bool isMoving = false;      // 이동 중인지 여부
    private Vector3 initialPosition;     // 초기 위치 저장
    public LayerMask playerLayer;       // 플레이어 레이어 설정

    private void Start()
    {
        initialPosition = transform.position; // 시작 위치 저장
    }

    private void Update()
    {
        baseMoveSpeed = GameManager.instance.GetMovementSpeed(); // BPM에 따른 속도 업데이트
        
        // 플레이어가 범위 안에 있는지 확인
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, GetCurrentRange(), playerLayer);
        
        if (playerInRange.Length > 0)
        {
            Debug.Log("범위 안에 들어옴!");
            isMoving = true; // 이동 시작
        }
        else
        {
            isMoving = false; // 이동 중지
        }

        // 이동 처리
        if (isMoving)
        {
            MoveCylinder();
        }
    }
    
    private void MoveCylinder()
    {
        float moveSpeed = baseMoveSpeed * (GameManager.instance.bpm / 120f);

        // 이동 거리 체크
        if (Vector3.Distance(transform.position, initialPosition) < moveDistance)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        }
        else
        {
            // 원래 위치로 돌아가는 로직
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, moveSpeed * Time.deltaTime);
        }
    }

    private float GetCurrentRange()
    {
        float adjustedRange = baseRange * (GameManager.instance.bpm / 120f);
        return Mathf.Clamp(adjustedRange, minRange, maxRange); // minRange와 maxRange를 설정해야 함
    }

    // 디버그용 Gizmos
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, GetCurrentRange());
    }
}
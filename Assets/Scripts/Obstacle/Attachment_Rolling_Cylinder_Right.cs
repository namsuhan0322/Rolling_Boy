using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment_Rolling_Cylinder_Right : MonoBehaviour
{
    public float range = 5f;  // 감지 범위
    public float moveSpeed = 2f; // 이동 속도
    public float moveDistance = 5f; // 이동 거리
    private bool isMoving = false; // 이동 중인지 여부
    private Vector3 initialPosition; // 초기 위치 저장
    public LayerMask playerLayer;  // 플레이어 레이어 설정

    private void Start()
    {
        initialPosition = transform.position; // 시작 위치 저장
    }

    private void Update()
    {
        // 플레이어가 범위 안에 있는지 확인
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, range, playerLayer);
        
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
            RightMoveCylinder();
        }
    }
    
    private void RightMoveCylinder()
    {
        if (Vector3.Distance(transform.position, initialPosition) < moveDistance)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World); // x축 방향으로 이동
        }
    }
}
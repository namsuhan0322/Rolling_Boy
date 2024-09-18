using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float range = 5f;  // 감지 범위
    private Animator anim;     // 애니메이터 변수
    public LayerMask playerLayer;  // 플레이어 레이어 설정 (태그 대신)

    private void Start()
    {
        anim = GetComponent<Animator>();  // 애니메이터 컴포넌트 가져오기
    }

    private void Update()
    {
        // 주위에 플레이어가 있는지 확인하는 구 탐지 (3D)
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, range, playerLayer);
        
        if (playerInRange.Length > 0)
        {
            Debug.Log("범위 안에 들어옴!");
            // 플레이어가 범위 내에 있을 경우 애니메이션 실행
            anim.SetBool("isOpening", true);
        }
        else
        {
            // 플레이어가 범위 내에 없을 경우 애니메이션 중지
            anim.SetBool("isOpening", false);
        }
    }

    // 범위를 확인할 수 있는 디버그용 구 그리기
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
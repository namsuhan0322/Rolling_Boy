using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float range = 5f;  // 감지 범위
    private float currentRange;     // 현재 감지 범위
    private Animator anim;     // 애니메이터 변수
    public LayerMask playerLayer;  // 플레이어 레이어 설정 (태그 대신)
    
    // 레이저 프리팹 오브젝트
    public GameObject laserPrefab;  // 레이저 프리팹

    // 레이저 프리팹 안의 레이저빔 자식 오브젝트
    private GameObject laserBeam;
    
    public float animationDuration = 2f;

    private void Start()
    {
        anim = GetComponent<Animator>();  // 애니메이터 컴포넌트 가져오기
        
        // 레이저 프리팹의 자식 오브젝트(레이저빔) 찾기
        if (laserPrefab != null)
        {
            laserBeam = laserPrefab.transform.Find("Beam").gameObject;  // 레이저빔 자식 오브젝트의 이름을 입력
        }
    }

    private void Update()
    {
        // BPM에 따라 현재 범위 조정
        currentRange = range * (GameManager.instance.bpm / 120f); // 기본 범위를 BPM에 따라 조정
        
        // 주위에 플레이어가 있는지 확인하는 구 탐지 (3D)
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, currentRange, playerLayer);
        
        if (playerInRange.Length > 0)
        {
            // 플레이어가 범위 내에 있을 경우 애니메이션 실행
            anim.SetBool("isOpening", true);
            
            // 애니메이션 속도에 따라 지연 시간 계산
            float delay = (90f / 60f) / (anim.speed); // 1.5초를 현재 애니메이션 속도로 나눈 값
            
            // 코루틴을 통해 일정 시간 후에 레이저빔을 활성화
            StartCoroutine(ActivateLaserAfterDelay(delay));
        }
        else
        {
            // 플레이어가 범위 내에 없을 경우 애니메이션 중지
            anim.SetBool("isOpening", false);
            // 레이저빔 끄기
            if (laserBeam != null)
            {
                laserBeam.SetActive(false);
            }
        }
    }

    // 일정 시간 후에 레이저 빔을 활성화하는 코루틴
    private IEnumerator ActivateLaserAfterDelay(float delay)
    {
        // 애니메이션이 끝날 때까지 기다림
        yield return new WaitForSeconds(delay);
        
        // 레이저 빔 활성화
        if (laserBeam != null)
        {
            laserBeam.SetActive(true);
        }
    }

    // 범위를 확인할 수 있는 디버그용 구 그리기
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

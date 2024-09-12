using UnityEngine;

public class MoveTile : MonoBehaviour
{
    private Vector3 start;  // 이동 시작 위치
    private Vector3 end;    // 이동 끝 위치
    
    private float distance = 4.0f;  // 이동 거리 (단위: 유니티 유닛)
    private float startTime;    // 이동 시작 시간
    
    [SerializeField] private float speed = 2.0f;    // 이동 속도 (단위: 유니티 유닛/초)

    void Start()
    {
        start = transform.position; // 현재 위치를 이동 시작 위치로 설정
        end = start + new Vector3(distance, 0, 0);  // 이동 끝 위치를 설정 (start 위치에서 distance 만큼 이동한 위치)
        startTime = Time.time;  // 이동 시작 시간을 현재 시간으로 설정
    }

    void Update()
    {
        // 이동 비율 계산 (0에서 1 사이의 값)
        float fraction = (Time.time - startTime) * speed / distance;
        
        // 현재 위치를 start와 end 위치 사이에서 비율에 따라 보간하여 계산
        transform.position = Vector3.Lerp(start, end, fraction);
        
        // 현재 위치가 end 위치에 도달하면 이동 방향을 반전
        if (transform.position == end) {
            Vector3 aux = start;
            start = end;
            end = aux;

            // 이동 시작 시간 갱신
            startTime = Time.time;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public float speed = 5.0f;  // 타일이 이동하는 속도

    void Update()
    {
        // Z축 방향으로 속도에 맞춰 타일 이동
        transform.Translate(0, 0, -speed * Time.deltaTime);

        // 타일이 일정 거리만큼 이동하면 폴로 돌려주기 위한 조건
        if (transform.position.z < -50.0f)  // 예: Z축 -50을 넘으면
        {
            // 타일을 비활성화하거나 폴에 반환
            gameObject.SetActive(false);
        }
    }
}

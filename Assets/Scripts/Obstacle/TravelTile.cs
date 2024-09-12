using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelTile : MonoBehaviour
{
    // 현재 타일이 들고 있는 객체 (플레이어)
    private GameObject holdingObject;

    void Update()
    {
        // 타일이 객체를 들고 있을 때, 객체의 이동 속도에 따라 타일을 이동시킴
        if (holdingObject != null) {
            // 플레이어의 이동 속도에 따라 타일을 Z축 방향으로 이동
            transform.Translate(0.0f, 0.0f, holdingObject.GetComponent<PlayerController>().speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 타일이 지면과 충돌하고, 충돌 지점이 타일의 X축 위치와 같은 경우
        if (collision.gameObject.CompareTag("Ground") && collision.transform.position.x == transform.position.x) {
            // 타일이 들고 있는 객체를 해제
            holdingObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 "Player" 태그를 가진 경우
        if (other.gameObject.CompareTag("Player")) {
            // 타일이 플레이어를 들고 있음
            holdingObject = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 충돌한 객체가 "Player" 태그를 가진 경우
        if (other.gameObject.CompareTag("Player")) {
            // 타일이 들고 있는 객체를 해제
            holdingObject = null;
        }
    }
}
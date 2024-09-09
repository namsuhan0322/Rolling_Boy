using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어 움직임")]
    public float speed = 10.0f;
    private Rigidbody playerRigidbody;
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float xSpeed = xInput * speed;
        
        Vector3 newVelocity = new Vector3(xSpeed, playerRigidbody.velocity.y, 0);
        
        playerRigidbody.velocity = newVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ground와 충돌하면
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Ground와 충돌 처리 로직이 있다면 여기에 추가
        }

        // JumpPad와 충돌하면
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            // 점프 패드의 점프 힘을 받아서 점프
            JumpPad jumpPad = collision.gameObject.GetComponent<JumpPad>();

            // JumpPad의 점프 힘을 이용해 위쪽으로 힘을 가함
            playerRigidbody.AddForce(Vector3.up * jumpPad.jumpPadForce, ForceMode.Impulse);
        }
    }
}
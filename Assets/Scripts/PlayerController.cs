using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어")]
    public float speed = 5f;

    private float currentAcceleration = 0;
    private Rigidbody playerRigidbody;
    
    [SerializeField] private float maxAccSide = 6;
    [SerializeField] private float accelerationRate = 20;
    
    private Vector3 iniScale;
    private Vector3 moveSideScale;
    
    public bool airborne = false;
    
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        iniScale = transform.localScale;
        moveSideScale = iniScale - new Vector3(iniScale.x / 8, 0, 0);
    }
    
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            if (Mathf.Abs(currentAcceleration) < 0.1) currentAcceleration = 0;
            if (currentAcceleration < 0) currentAcceleration += Time.deltaTime * accelerationRate / 1.25f;
            else if (currentAcceleration > 0) currentAcceleration -= Time.deltaTime * accelerationRate / 1.25f;
            MoveSideScale(false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Mathf.Abs(currentAcceleration) < maxAccSide || currentAcceleration > 0) currentAcceleration -= Time.deltaTime * accelerationRate;
            MoveSideScale(true);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Mathf.Abs(currentAcceleration) < maxAccSide || currentAcceleration < 0) currentAcceleration += Time.deltaTime * accelerationRate;
            MoveSideScale(true);
        }
        transform.Translate(currentAcceleration * Time.deltaTime, 0.0f, 0.0f);
        
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }
    
    private void MoveSideScale (bool moveSide)
    {
        if (moveSide) transform.localScale = Vector3.Lerp(transform.localScale, moveSideScale, Time.deltaTime * maxAccSide);
        else transform.localScale = Vector3.Lerp(transform.localScale, iniScale, Time.deltaTime * maxAccSide);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*// JumpPad와 충돌하면
        if (collision.gameObject.CompareTag("JumpPad"))
        {
            // 점프 패드의 점프 힘을 받아서 점프
            JumpPad jumpPad = collision.gameObject.GetComponent<JumpPad>();

            // JumpPad의 점프 힘을 이용해 위쪽으로 힘을 가함
            playerRigidbody.AddForce(Vector3.up * jumpPad.jumpPadForce, ForceMode.Impulse);
        }*/

        // Enemy와 충돌하면
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 플레이어가 죽음
            gameObject.SetActive(false);
        }
    }
}
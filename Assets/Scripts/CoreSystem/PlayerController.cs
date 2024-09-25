using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어")]
    public float speed;

    private float currentAcceleration = 0;
    private Rigidbody playerRigidbody;

    [SerializeField] private float maxAccSide = 6;
    [SerializeField] private float accelerationRate = 20;

    private Vector3 iniScale;
    private Vector3 moveSideScale;

    public bool airborne = false;

    private CameraMove cameraMove;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        iniScale = transform.localScale;
        moveSideScale = iniScale - new Vector3(iniScale.x / 8, 0, 0);
        cameraMove = Camera.main.GetComponent<CameraMove>(); // CameraMove 컴포넌트를 찾기
    }

    void Update()
    {
        speed = GameManager.instance.GetMovementSpeed(); // BPM에 따른 속도 업데이트
        Move();

        if (gameObject.transform.position.y <= -2.5f)
        {
            Die();
        }
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
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime); // BPM에 따라 속도 적용
    }

    private void MoveSideScale(bool moveSide)
    {
        if (moveSide) transform.localScale = Vector3.Lerp(transform.localScale, moveSideScale, Time.deltaTime * maxAccSide);
        else transform.localScale = Vector3.Lerp(transform.localScale, iniScale, Time.deltaTime * maxAccSide);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }
    
    void Die()
    {
        gameObject.SetActive(false);
        cameraMove.shake = true;
    }
}
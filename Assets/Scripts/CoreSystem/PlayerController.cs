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
    public bool isInvulnerable = false; // 무적 상태를 나타내는 변수
    public float invulnerabilityDuration = 2f; // 무적 지속 시간

    private CameraMove cameraMove;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        iniScale = transform.localScale;
        moveSideScale = iniScale - new Vector3(iniScale.x / 8, 0, 0);
        cameraMove = Camera.main.GetComponent<CameraMove>();
    }

    void Update()
    {
        speed = GameManager.instance.GetMovementSpeed();
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
        transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    }

    private void MoveSideScale(bool moveSide)
    {
        if (moveSide) transform.localScale = Vector3.Lerp(transform.localScale, moveSideScale, Time.deltaTime * maxAccSide);
        else transform.localScale = Vector3.Lerp(transform.localScale, iniScale, Time.deltaTime * maxAccSide);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable) // 무적 상태가 아닐 때만 데미지
        {
            Die();
        }
    }
    
    void Die()
    {
        gameObject.SetActive(false);
        cameraMove.shake = true;
    }

    public void StartInvulnerability()
    {
        StartCoroutine(InvulnerabilityCoroutine());
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true; // 무적 상태로 설정
        // 플레이어의 외형이나 효과 추가: 예를 들어, 색상을 변경하거나 애니메이션 추가

        yield return new WaitForSeconds(invulnerabilityDuration); // 지정된 시간 동안 대기

        isInvulnerable = false; // 무적 상태 해제
        // 무적 해제 후 외형 복원
    }
}

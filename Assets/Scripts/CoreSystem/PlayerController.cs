using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float speed;
    [SerializeField] private bool isSpeed = false;
    [SerializeField] private bool isPlayingMusic = false;

    private float currentAcceleration = 0;

    [SerializeField] private float maxAccSide = 6;
    [SerializeField] private float accelerationRate = 20;

    private Vector3 iniScale;
    private Vector3 moveSideScale;

    public bool airborne = false;
    public bool isInvulnerable = false; // 무적 상태를 나타내는 변수
    public float invulnerabilityDuration = 0.5f; // 무적 지속 시간

    private CameraMove cameraMove;
    private Rigidbody playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        iniScale = transform.localScale;
        moveSideScale = iniScale - new Vector3(iniScale.x / 8, 0, 0);
        cameraMove = Camera.main.GetComponent<CameraMove>();
    }

    void Update()
    {
        if (!GameStateManager.instance.isGameRunning) return; // 게임이 시작되지 않았다면 아무것도 하지 않음

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
            
            if (currentAcceleration < 0)
            {
                currentAcceleration += Time.deltaTime * accelerationRate / 1.25f;
            }
            else if (currentAcceleration > 0)
            {
                currentAcceleration -= Time.deltaTime * accelerationRate / 1.25f;
            }
            MoveSideScale(false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Mathf.Abs(currentAcceleration) < maxAccSide || currentAcceleration > 0)
            {
                currentAcceleration -= Time.deltaTime * accelerationRate;
            }
            MoveSideScale(true);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Mathf.Abs(currentAcceleration) < maxAccSide || currentAcceleration < 0)
            {
                currentAcceleration += Time.deltaTime * accelerationRate;
            }
            MoveSideScale(true);
        }

        transform.Translate(currentAcceleration * Time.deltaTime, 0.0f, 0.0f);

        if (!isSpeed)
        {
            transform.Translate(0.0f, 0.0f, speed * Time.deltaTime);
    
            UiManager2 uiManager2 = FindObjectOfType<UiManager2>();

            // 음악이 재생 중이지 않고 체크포인트가 설정되지 않았을 때만 재생
            if (!isPlayingMusic && !uiManager2.IsCheckpointSet())
            {
                SoundManager.instance.PlaySound("Game1");
                isPlayingMusic = true;
            }
            // 체크포인트가 설정된 경우, 저장된 시간부터 음악 재개
            else if (!isPlayingMusic && uiManager2.IsCheckpointSet())
            {
                SoundManager.instance.ResumeMusicFromSavedTime("Game1");
                isPlayingMusic = true;
            }
        }
        else
        {
            isPlayingMusic = false; // 멈출 때 플래그를 초기화
        }
    }

    private void MoveSideScale(bool moveSide)
    {
        if (moveSide)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, moveSideScale, Time.deltaTime * maxAccSide);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, iniScale, Time.deltaTime * maxAccSide);
        }
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
        SoundManager.instance.StopSound("Game1");
    }

    public void StartInvulnerability()
    {
        StartCoroutine(InvulnerabilityCoroutine());
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true; // 무적 상태로 설정

        yield return new WaitForSeconds(invulnerabilityDuration); // 지정된 시간 동안 대기

        isInvulnerable = false; // 무적 상태 해제
    }
}
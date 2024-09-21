using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBPM : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // BPM에 따라 애니메이션 속도 조정
        animator.speed = GameManager.instance.bpm / 120f; // 기본 BPM에 비례하여 속도 조정
    }
}

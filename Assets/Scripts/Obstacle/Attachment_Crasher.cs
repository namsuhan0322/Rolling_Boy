using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment_Crasher : MonoBehaviour
{
    void Start()
    {
        // 회전을 (90, 0, 0)으로 설정
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}

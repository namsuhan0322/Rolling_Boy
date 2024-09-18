using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment_RollingCylinder : MonoBehaviour
{
    void Start()
    {
        // 위치를 (0, 0.5f, 0)로 설정
        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        // 회전을 (90, 0, 0)으로 설정
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}

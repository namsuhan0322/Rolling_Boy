using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attachment_Laser : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
    }
}

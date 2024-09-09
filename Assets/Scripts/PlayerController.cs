using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어 움직임")]
    public float speed = 5f;
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
}

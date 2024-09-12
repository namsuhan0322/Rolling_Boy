using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTile : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("떨어지다");
        }
    }
}

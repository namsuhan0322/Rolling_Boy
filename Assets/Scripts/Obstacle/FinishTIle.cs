using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTIle : MonoBehaviour
{
    public bool isFinishTile = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isFinishTile && other.CompareTag("Player"))
        {
            SoundManager.instance.StopSound("Game1");
            Time.timeScale = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnScene : MonoBehaviour
{
    public IEnumerator ReturnMainScene()
    {
        yield return new WaitForSeconds(3f);
        SoundManager.instance.StopSound("Game1");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCheckpoint : MonoBehaviour
{
    public LayerMask isCheckPoint;
    public Collider[] findedCheckPoint;
    public bool isReset = false;
    public bool isFinded = false;

    // Update is called once per frame
    /*void Update()
    {
        //Vector3 center = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 15);
        Vector3 halfExtents = new Vector3(5, 20, 30); // 테스트여서 높이가 20


        findedCheckPoint = Physics.OverlapBox(transform.position, halfExtents, Quaternion.identity, isCheckPoint);
        if (findedCheckPoint != null)
        {
            isFinded = true;
        }

        for (int i = 0; i < findedCheckPoint.Length; i++)
        {
            Debug.Log(findedCheckPoint[i]);
        }


        if (isReset)//초기화
        {
            for (int i = 0; i < findedCheckPoint.Length; i++)
            {
                findedCheckPoint[i] = null;
            }
            isReset = false;
        }
    }*/
}

using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject tilePrefab; // 생성할 타일의 프리팹
    public int poolSize = 10; // 풀 안에 미리 생성할 오브젝트 수
    private Queue<GameObject> poolQueue; // 오브젝트를 담을 큐

    void Start()
    {
        poolQueue = new Queue<GameObject>();

        // 미리 타일 오브젝트를 풀에 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(tilePrefab);
            obj.SetActive(false); // 처음에는 비활성화 상태로
            poolQueue.Enqueue(obj); // 큐에 추가
        }
    }

    // 오브젝트를 풀에서 가져오는 함수
    public GameObject GetTile()
    {
        if (poolQueue.Count > 0)
        {
            GameObject obj = poolQueue.Dequeue();
            obj.SetActive(true); // 오브젝트 활성화
            return obj;
        }
        else
        {
            // 필요하다면 새로운 오브젝트를 생성해 풀에 추가할 수 있음
            GameObject obj = Instantiate(tilePrefab);
            return obj;
        }
    }

    // 오브젝트를 다시 풀로 반환하는 함수
    public void ReturnTile(GameObject obj)
    {
        obj.SetActive(false); // 비활성화 상태로
        poolQueue.Enqueue(obj); // 큐에 다시 추가
    }
}

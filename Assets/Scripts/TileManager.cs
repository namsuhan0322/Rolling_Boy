using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public ObjectPool objectPool;       // ObjectPool을 참조
    private double spawnZ = -3.02;      // 타일을 생성할 Z 위치
    private double tileLength = 0.98;   // 타일의 길이
    public float tileMoveSpeed = 5.0f;  // 타일의 이동 속도
    public int numTilesOnScreen = 7;    // 화면에 표시되는 타일 개수
    
    private List<GameObject> activeTiles = new List<GameObject>(); // 활성화된 타일 리스트

    void Start()
    {
        // 초기 타일 세팅 (화면에 필요한 만큼 타일을 미리 생성)
        for (int i = 0; i < numTilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        MoveTiles();    // 타일을 -Z축 방향으로 이동
        CheckAndSpawnTiles();  // 새로운 타일 생성 여부 체크
        ReturnTiles();  // 화면에서 사라진 타일 반환
    }

    // 타일을 -Z축 방향으로 이동시키는 함수
    void MoveTiles()
    {
        foreach (GameObject tile in activeTiles)
        {
            tile.transform.Translate(0, 0, -tileMoveSpeed * Time.deltaTime);
        }
    }

    // 타일이 일정 거리까지 갔으면 새로운 타일을 생성하는 함수
    void CheckAndSpawnTiles()
    {
        // 활성화된 마지막 타일의 Z축 위치 확인
        if (activeTiles.Count > 0 && activeTiles[activeTiles.Count - 1].transform.position.z < 30.0f)
        {
            SpawnTile();
        }
    }

    // 타일을 생성하는 함수
    void SpawnTile()
    {
        GameObject tile = objectPool.GetTile();  // Object Pool에서 타일 가져오기
        tile.transform.position = new Vector3(0, 0, (float)spawnZ); // Z축 위치에 타일 배치
        activeTiles.Add(tile);  // 활성화된 타일 목록에 추가
        spawnZ += tileLength;   // 다음 타일을 생성할 Z 위치 업데이트
        
        Debug.Log($"spawnZ : {spawnZ}");
        Debug.Log($"tileLength : {tileLength}");
    }

    // 일정 거리 이상 떨어진 타일을 반환하는 함수
    void ReturnTiles()
    {
        if (activeTiles.Count > 0 && activeTiles[0].transform.position.z < -10.0f)
        {
            // 제일 오래된 타일을 Object Pool로 반환하고 리스트에서 제거
            objectPool.ReturnTile(activeTiles[0]);

            // 반환 시 타일의 Z 값을 초기화
            spawnZ = activeTiles[activeTiles.Count - 1].transform.position.z + tileLength;

            activeTiles.RemoveAt(0);
        }
    }

}
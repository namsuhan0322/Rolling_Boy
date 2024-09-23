using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private FileInfo sourceFile = new FileInfo("Assets/Levels/Level1.txt");
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private GameObject destinationObject;
    [SerializeField] private GameObject mainCamera;
    public float viewDistance = 50f; // 플레이어의 시야 거리

    private List<GameObject> createdTiles = new List<GameObject>();

    void Start()
    {
        if (GameManager.instance != null)
        {
            sourceFile = new FileInfo("Assets/Levels/Level" + GameManager.instance.currentLevel + ".txt");
        }

        StreamReader reader = sourceFile.OpenText();
        string text = reader.ReadLine();

        for (int i = 0; text != null; ++i) 
        {
            createRow(i, text);
            text = reader.ReadLine();
        }

        UpdateTileVisibility(); // 시작 시 타일 가시성 업데이트
    }

    private void Update()
    {
        UpdateTileVisibility(); // 매 프레임 타일 가시성 업데이트
    }

    private void createRow(int zPos, string rowInfo) 
    {
        for (int i = 0; i < 5; i++) 
        {
            int tileType = rowInfo[i] - 'a';
            if (tileType >= 0 && tileType < tiles.Length)
            {
                GameObject tile = Instantiate(tiles[tileType], new Vector3(i, 0, zPos), Quaternion.identity, destinationObject.transform);
                tile.SetActive(false); // 초기 생성 시 비활성화
                createdTiles.Add(tile); // 생성한 타일을 리스트에 추가
            }
        }
    }

    private void UpdateTileVisibility()
    {
        Vector3 playerPosition = mainCamera.transform.position;

        foreach (GameObject tile in createdTiles)
        {
            float distance = Vector3.Distance(playerPosition, tile.transform.position);

            // 카메라의 z축을 기준으로 타일이 뒤에 있는지 확인
            bool isBehindPlayer = tile.transform.position.z < playerPosition.z;

            // 타일의 가시성 결정: 시야 거리 이내이면서 플레이어 뒤가 아니어야 활성화
            tile.SetActive(distance <= viewDistance && !isBehindPlayer);
        }
    }
}

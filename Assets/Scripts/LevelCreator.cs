using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private FileInfo sourceFile = new FileInfo("Assets/Levels/Level1.txt");
    [SerializeField] private GameObject[] tiles;
    [SerializeField] private GameObject destinationObject;
    [SerializeField] private GameObject mainCamera;

    public int levelLength { get; private set; } = 0;
    
    void Start()
    {
        if (GameManager.instance != null)
        {
            sourceFile = new FileInfo("Assets/Levels/Level" + GameManager.instance.currentLevel + ".txt");
        }
        
        StreamReader reader = sourceFile.OpenText();
        string text = reader.ReadLine();

        for (int i = 0; text != null; ++i) {
            createRow(i, text);
            text = reader.ReadLine();
            levelLength = i;
        }
    }

    private void createRow (int zPos, string rowInfo) 
    {
        switch (rowInfo[0]) 
        {
            case 'c':
                createTile(zPos, 0, rowInfo[0] - 'a');
                break;
            case 'd':
                createTile(zPos, 0, rowInfo[0] - 'a');
                break;
            default:
                for (int i = 0; i < 5; i++) {
                    createTile(zPos, i, rowInfo[i] - 'a');
                }
                break;
        }
    }

    private void createTile (int zPos, int xPos, int tileType) 
    {
        if (tileType >= 0 && tileType < tiles.Length)
        {
            Instantiate(tiles[tileType], new Vector3(xPos, 0, zPos), Quaternion.identity, destinationObject.transform);
        }
    }
}

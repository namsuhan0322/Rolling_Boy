using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{
    [Header("Skybox Material 목록")]
    public Material[] skyboxMaterials;

    private int currentBackgroundIndex = 0;

    private void Start()
    {
        // 초기 Skybox 설정
        RenderSettings.skybox = skyboxMaterials[currentBackgroundIndex];
        DynamicGI.UpdateEnvironment();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 타일을 밟았을 때
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 트리거 존에 들어왔습니다.");
            ChangeSkybox(); // 배경 변경 함수 호출
        }
    }
    
    public void ChangeSkybox()
    {
        Debug.Log("현재 인덱스: " + currentBackgroundIndex); // 현재 인덱스 로그
    
        // Skybox를 다음 Material로 변경
        currentBackgroundIndex = (currentBackgroundIndex + 1) % skyboxMaterials.Length;
    
        Debug.Log("다음 인덱스: " + currentBackgroundIndex); // 다음 인덱스 로그
        RenderSettings.skybox = skyboxMaterials[currentBackgroundIndex];

        Debug.Log("Skybox가 변경되었습니다: " + skyboxMaterials[currentBackgroundIndex].name);

        // 환경광 업데이트
        DynamicGI.UpdateEnvironment(); // Skybox 변경 사항을 즉시 반영
    }
}
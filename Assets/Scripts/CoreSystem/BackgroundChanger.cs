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
            ChangeSkybox(); // 배경 변경 함수 호출
        }
    }
    
    public void ChangeSkybox()
    {
        // Skybox를 다음 Material로 변경
        currentBackgroundIndex = (currentBackgroundIndex + 1) % skyboxMaterials.Length;
        RenderSettings.skybox = skyboxMaterials[currentBackgroundIndex];

        // 환경광 업데이트
        DynamicGI.UpdateEnvironment(); // Skybox 변경 사항을 즉시 반영
    }
}
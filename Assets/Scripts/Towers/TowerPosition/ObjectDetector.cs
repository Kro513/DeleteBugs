using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private UIUpgrade uIUpgrade;
    [SerializeField] private TowerSpawner towerSpawner;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition); // 카메라 위치 -> 마우스 위치 관통 광선 생성

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // "Tile" 태그 오브젝트에 부딪히면
                if (hit.transform.CompareTag("Tile")) 
                {
                    Debug.Log("hit");
                    towerSpawner.SpawnTower(hit.transform); // 타워 생성 함수 호출
                }
                else if (hit.transform.CompareTag("Tower"))
                {
                    uIUpgrade.OnUpgradeImg();

                }
            }
        }
       
    }
    
}

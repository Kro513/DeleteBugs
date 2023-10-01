using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private UIUpgrade uIUpgrade;

    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;

    private Tower myTower;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

     public void SpawnTower(Transform tileTransform)
     {
         Tile tile = tileTransform.GetComponent<Tile>();

         // 타워 건설 가능 여부 확인
         if (tile.IsBulidTower == true) // 현재 타일에 타워가 이미 설치되어 있으면
         {
             return; // 타원 건설X
         }
         if (UIManager.Instance.ClickedBtn != null)
         {
             tile.IsBulidTower = true; // 타원 건설되어 있음으로 설정
             GameObject tower = Instantiate(UIManager.Instance.ClickedBtn.TowerPrefab, tileTransform.position, Quaternion.identity); // 선택 위치 타워 생성
             this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();

             Hover_.Instance.Deactivate();

             UIManager.Instance.BuyTower();
         }
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
                    SpawnTower(hit.transform); // 타워 생성 함수 호출
                }
                else if (hit.transform.CompareTag("Tower")&& UIManager.Instance.ClickedBtn == null)
                {
                    uIUpgrade.OnUpgradeImg();

                    if (myTower != null)
                    {
                        Debug.Log("click");
                        UIManager.Instance.SelectTower(myTower);
                    }
                    else
                    {
                        UIManager.Instance.DeselectTower();
                    }

                }
            }
        }
       
    }
    
}

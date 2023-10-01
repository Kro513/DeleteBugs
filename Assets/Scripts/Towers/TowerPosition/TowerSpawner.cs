using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    /*private Tower myTower;
    private GameObject tower;
    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();

        // 타워 건설 가능 여부 확인
        if (tile.IsBulidTower == true) // 현재 타일에 타워가 이미 설치되어 있으면
        {
            return; // 타원 건설X
        }
        if(UIManager.Instance.ClickedBtn != null)
        {
            tile.IsBulidTower = true; // 타원 건설되어 있음으로 설정
            tower = Instantiate(UIManager.Instance.ClickedBtn.TowerPrefab, tileTransform.position, Quaternion.identity); // 선택 위치 타워 생성
            
            Hover_.Instance.Deactivate();

            UIManager.Instance.BuyTower();
        }
    }

    public void Towerplace()
    {
        this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();
    }*/
}

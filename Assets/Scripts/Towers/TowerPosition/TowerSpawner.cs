using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    //[SerializeField] private GameObject towerPrefab;
    [SerializeField] private EnemySpawner enemySpawner;

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
            
            GameObject clone = Instantiate(UIManager.Instance.ClickedBtn.TowerPrefab, tileTransform.position, Quaternion.identity); // 선택 위치 타워 생성
            clone.GetComponent<TowerWeapon>().Setup(enemySpawner);

            Hover_.Instance.Deactivate();
            UIManager.Instance.BuyTower();

            tile.IsBulidTower = true; // 타원 건설되어 있음으로 설정
        }
    }
}

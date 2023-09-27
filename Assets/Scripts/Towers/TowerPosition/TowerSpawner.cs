using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPrefab;

    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();

        // 타워 건설 가능 여부 확인
        if (tile.IsBulidTower == true) // 현재 타일에 타워가 이미 설치되어 있으면
        {
            return; // 타원 건설X
        }

        tile.IsBulidTower = true; // 타원 건설되어 있음으로 설정
        /*GameObject newTower = */Instantiate(towerPrefab, tileTransform.position, Quaternion.identity); // 선택 위치 타워 생성
        /*newTower.transform.SetParent(parentTransform);*/
        
        //Instantiate(towerPrefab[], tileTransform.position, Quaternion.identity); // 선택 위치 타워 생성
    }
}

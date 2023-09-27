using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    //[SerializeField] private Transform parentTransform;

    public void SpawnTower(Transform tileTransform)
    {
        Tile tile = tileTransform.GetComponent<Tile>();

        // Ÿ�� �Ǽ� ���� ���� Ȯ��
        if (tile.IsBulidTower == true) // ���� Ÿ�Ͽ� Ÿ���� �̹� ��ġ�Ǿ� ������
        {
            return; // Ÿ�� �Ǽ�X
        }

        tile.IsBulidTower = true; // Ÿ�� �Ǽ��Ǿ� �������� ����
        /*GameObject newTower = */Instantiate(towerPrefab, tileTransform.position, Quaternion.identity); // ���� ��ġ Ÿ�� ����
        /*newTower.transform.SetParent(parentTransform);*/
        
    }
}
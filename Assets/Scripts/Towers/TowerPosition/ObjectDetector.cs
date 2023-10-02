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
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition); // ī�޶� ��ġ -> ���콺 ��ġ ���� ���� ����

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                // "Tile" �±� ������Ʈ�� �ε�����
                if (hit.transform.CompareTag("Tile")) 
                {
                    Debug.Log("hit");
                    towerSpawner.SpawnTower(hit.transform); // Ÿ�� ���� �Լ� ȣ��
                }
                else if (hit.transform.CompareTag("Tower"))
                {
                    uIUpgrade.OnUpgradeImg();

                }
            }
        }
       
    }
    
}

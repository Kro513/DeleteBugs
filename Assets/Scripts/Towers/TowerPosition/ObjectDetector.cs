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

         // Ÿ�� �Ǽ� ���� ���� Ȯ��
         if (tile.IsBulidTower == true) // ���� Ÿ�Ͽ� Ÿ���� �̹� ��ġ�Ǿ� ������
         {
             return; // Ÿ�� �Ǽ�X
         }
         if (UIManager.Instance.ClickedBtn != null)
         {
             tile.IsBulidTower = true; // Ÿ�� �Ǽ��Ǿ� �������� ����
             GameObject tower = Instantiate(UIManager.Instance.ClickedBtn.TowerPrefab, tileTransform.position, Quaternion.identity); // ���� ��ġ Ÿ�� ����
             this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();

             Hover_.Instance.Deactivate();

             UIManager.Instance.BuyTower();
         }
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
                    SpawnTower(hit.transform); // Ÿ�� ���� �Լ� ȣ��
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDayNight : MonoBehaviour
{
   
    
        public GameObject Sun;
        public GameObject Moon;

        void Start()
        {
            // �ʱ� ����
            SetDay(true); // �� ������ ǥ��
            
        }

        // �� UI ����
        public void SetDay(bool isActive)
        {
            Sun.SetActive(isActive);
            Moon.SetActive(false);
        }

        // �� UI ����
        public void SetNight(bool isActive)
        {
            //UIManager.Instance.uiWaves.UpdateWaveUI();
            Moon.SetActive(isActive);
            Sun.SetActive(false);
        }
    

}

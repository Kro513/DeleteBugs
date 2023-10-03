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
            int _currentWave = GameManager.Instance._currentWave;
            GameManager.Instance.player.GetGold(_currentWave * 100);
        }

        // �� UI ����
        public void SetNight(bool isActive)
        {
            GameManager.Instance.UpdateWaveUI();
            Moon.SetActive(isActive);
            Sun.SetActive(false);
        }
    

}

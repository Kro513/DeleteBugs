using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDayNight : MonoBehaviour
{
   
    
        public GameObject Sun;
        public GameObject Moon;

        void Start()
        {
            // 초기 설정
            SetDay(true); // 낮 아이콘 표시
            
        }

        // 낮 UI 설정
        public void SetDay(bool isActive)
        {
            Sun.SetActive(isActive);
            Moon.SetActive(false);
            int _currentWave = GameManager.Instance._currentWave;
            GameManager.Instance.player.GetGold(_currentWave * 100);
        }

        // 밤 UI 설정
        public void SetNight(bool isActive)
        {
            GameManager.Instance.UpdateWaveUI();
            Moon.SetActive(isActive);
            Sun.SetActive(false);
        }
    

}

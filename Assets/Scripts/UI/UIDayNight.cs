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
        }

        // 밤 UI 설정
        public void SetNight(bool isActive)
        {
            //UIManager.Instance.uiWaves.UpdateWaveUI();
            Moon.SetActive(isActive);
            Sun.SetActive(false);
        }
    

}

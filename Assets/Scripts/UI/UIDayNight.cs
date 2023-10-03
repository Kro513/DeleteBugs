using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDayNight : MonoBehaviour
{
   
    
        public GameObject Sun;
        public GameObject Moon;

        void Start()
        {
            
        }

        // �� UI ����
        public void SetDay(bool isActive)
        {
            Sun.SetActive(isActive);
            Moon.SetActive(false);
            int _currentWave = GameManager.Instance._currentWave;
            GameManager.Instance.player.GetGold(_currentWave * 100);

        if (isActive)
        {
            SoundManager.instance.PlayDayMusic(); // �� ���¿����� �� ��� ���
        }
    }

        // �� UI ����
        public void SetNight(bool isActive)
        {
            GameManager.Instance.UpdateWaveUI();
            GameManager.Instance.waveSystem.StartWave();
            Moon.SetActive(isActive);
            Sun.SetActive(false);
        if (isActive)
        {
            SoundManager.instance.PlayNightMusic(); // �� ���¿����� �� ��� ���
        }
    }

    

}

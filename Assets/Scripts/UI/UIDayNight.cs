using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDayNight : MonoBehaviour
{

        

        public GameObject Sun;
        public GameObject Moon;
        int SetDayCount = 0;
        void Start()
        {
        SoundManager.instance.PlayDayMusic(); // �� ���¿����� �� ��� ���

        }

    // �� UI ����
    public void SetDay(bool isActive)
        {
            Sun.SetActive(isActive);
            Moon.SetActive(false);
            int _currentWave = GameManager.Instance._currentWave;
            UIManager.Instance.GetGold(_currentWave * 100);
            if (isActive)
            {
            SoundManager.instance.PlayDayMusic(); // �� ���¿����� �� ��� ���
            }
        SetDayCount++;
        int _hp = GameManager.Instance.player._currentHp;
        if (_hp > 0)
        {
            if (SetDayCount == 5)
            {
                    GameManager.Instance.GameClear();
            }
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

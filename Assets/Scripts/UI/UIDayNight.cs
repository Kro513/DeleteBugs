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
        SoundManager.instance.PlayDayMusic(); // ³· »óÅÂ¿¡¼­´Â ³· ºê±Ý Àç»ý

        }

    // ³· UI ¼³Á¤
    public void SetDay(bool isActive)
        {
            Sun.SetActive(isActive);
            Moon.SetActive(false);
            int _currentWave = GameManager.Instance._currentWave;
            UIManager.Instance.GetGold(_currentWave * 100);
            if (isActive)
            {
            SoundManager.instance.PlayDayMusic(); // ³· »óÅÂ¿¡¼­´Â ³· ºê±Ý Àç»ý
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

        // ¹ã UI ¼³Á¤
        public void SetNight(bool isActive)
        {
            GameManager.Instance.UpdateWaveUI();
            GameManager.Instance.waveSystem.StartWave();
            Moon.SetActive(isActive);
            Sun.SetActive(false);
            if (isActive)
            {
            SoundManager.instance.PlayNightMusic(); // ¹ã »óÅÂ¿¡¼­´Â ¹ã ºê±Ý Àç»ý
            }
        }

    

}

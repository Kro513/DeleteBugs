using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public Text waveText;
    public GameObject GameClearUI;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    public int _currentWave = 0;
    public Player player;
    public WaveSystem waveSystem;
    public UIDayNight uiDayNight;

   
    public void UpdateWaveUI()
    {
        _currentWave++;
        waveText.text = "Wave:" + (_currentWave);

        if(_currentWave > 5)
        {
            GameClear();
        }
    }

	private void GameClear()
	{
        GameClearUI.SetActive(true);
	}
}

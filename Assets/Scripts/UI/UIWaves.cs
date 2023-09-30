using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWaves : MonoBehaviour
{
    public Text waveText;
    private int currentWave = 0;
    // Start is called before the first frame update
    void Start()
    {
        UpdateWaveUI(currentWave);
    }

    // Update is called once per frame
    public void UpdateWaveUI(int waveNumber)
    {
        currentWave = waveNumber;
        waveText.text= "Wave:" + (currentWave + 1);
    }
}

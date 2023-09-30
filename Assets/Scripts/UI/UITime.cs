using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITime : MonoBehaviour
{
    public Text timerText;
    private float gameTime = 0f;

    private void Update()
    {
        // 게임 진행 시간을 업데이트하고 UI에 표시
        gameTime += Time.deltaTime;
        UpdateTimerUI(gameTime);
    }

    public void UpdateTimerUI(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}

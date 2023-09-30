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
        // ���� ���� �ð��� ������Ʈ�ϰ� UI�� ǥ��
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

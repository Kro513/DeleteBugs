using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseButton : MonoBehaviour
{
    private bool isPaused = false;
    private Button PauseButton;

    public GameObject PausePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void TogglePausePanel()
    {
        if (PausePanel != null)
        {
            isPaused = !isPaused;
            PausePanel.SetActive(isPaused);

            // 게임 일시 중지 상태에 따라 게임 로직을 정지 또는 재개할 수 있음
            TimePaused();
        }
    }
    public void ResumeGame()
    {
        if (PausePanel != null)
        {
            // Resume 버튼을 눌렀을 때 Pause 판넬을 비활성화하고 게임을 재개
            isPaused = false;
            PausePanel.SetActive(false);
            Time.timeScale = 1; // 게임 로직을 다시 재개
        }
    }

    public void TimePaused()
    {
        Time.timeScale = isPaused ? 0 : 1;
    }
}

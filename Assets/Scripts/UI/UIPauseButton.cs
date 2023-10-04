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

            // ���� �Ͻ� ���� ���¿� ���� ���� ������ ���� �Ǵ� �簳�� �� ����
            TimePaused();
        }
    }
    public void ResumeGame()
    {
        if (PausePanel != null)
        {
            // Resume ��ư�� ������ �� Pause �ǳ��� ��Ȱ��ȭ�ϰ� ������ �簳
            isPaused = false;
            PausePanel.SetActive(false);
            Time.timeScale = 1; // ���� ������ �ٽ� �簳
        }
    }

    public void TimePaused()
    {
        Time.timeScale = isPaused ? 0 : 1;
    }
}

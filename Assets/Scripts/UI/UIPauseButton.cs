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
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}

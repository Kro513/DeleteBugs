using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonStartScene : MonoBehaviour
{
    // �� �޼���� ��ư�� Ŭ���Ǿ��� �� ȣ��˴ϴ�.
    public void GoToStartScene()
    {
        // "StartScene"�� �̵��� ���� �̸��Դϴ�.
        SceneManager.LoadScene("StartScene");
    }
}

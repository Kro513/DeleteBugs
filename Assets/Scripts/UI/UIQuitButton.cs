using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuitButton : MonoBehaviour
{
    // Quit ��ư�� Ŭ���� �� ȣ���� �޼���
    public void QuitGame()
    {
        // ���� ����
        // Unity������ ����ũ�� �÷������� ���� ���� ������ �����ϴ� �� ���Ǵ� Application.Quit() �޼��带 �����մϴ�.
        // ��, �� �޼���� ����� ����ũ�� ���ø����̼ǿ����� �۵��ϸ� ������ ��忡���� �������� �ʽ��ϴ�. 
        //���� Ȯ���� ���غþ��.
        Application.Quit();
    }
}

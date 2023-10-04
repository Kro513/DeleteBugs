using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    public FlySwatter flySwatter; // FlySwatter ��ũ��Ʈ ������ ����

   [SerializeField] private Button upgradeButton;

    void Start()
    {
        upgradeButton = GetComponent<Button>(); // ��ư ������Ʈ ��������
        upgradeButton.onClick.AddListener(Upgrade); // ��ư Ŭ�� �̺�Ʈ�� Upgrade �޼��� ����
    }

    void Upgrade()
    {
        flySwatter.Upgrade(); // FlySwatter Ŭ������ Upgrade �޼��� ȣ��
        upgradeButton.interactable = false; // ��ư ��Ȱ��ȭ
    }
}

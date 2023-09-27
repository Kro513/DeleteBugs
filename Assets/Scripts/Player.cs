using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    public int _Gold;
    public int _currentHp;
    public int _maxHealth = 10;
    public Text PlayerHPDisplay;

    private void Awake()
    {
        _currentHp = _maxHealth;
        _Gold = 100;
        UpdateHealthUI();
    }

    // Start is called before the first frame update
   public void GetDamage(int damage)
    {
        _currentHp -= damage;
        UpdateHealthUI();

        if (_currentHp == 0)
        {
            
            //���� ���� UI ���
            //���ӿ��� ����Ʈ
            //���� ���
        }
        else
        {
            //Ÿ�� ���� ���
            //Ÿ�� ����Ʈ
        }


    }

    private void UpdateHealthUI()
    {
        PlayerHPDisplay.text = "���:" + _currentHp.ToString() + "/" + _maxHealth.ToString();
    }
    // Update is called once per frame

    public void RecoverHP()
    {
        //HPȸ��
    }
}

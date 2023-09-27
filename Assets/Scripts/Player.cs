using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    public int _Gold;
    public int _currentHp;
    public int _maxHealth = 10;
    //public Text PlayerHPDisplay;
    public Image[] heartImages;

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
            Debug.Log("���� ����!");
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

    public void UpdateHealthUI()
    {
        //PlayerHPDisplay.text = "���:" + _currentHp.ToString() + "/" + _maxHealth.ToString();

        for (int i = 0; i <= 9; i++)
        {
            // i ���� ���� ü�º��� ���� �� ��Ʈ �̹����� Ȱ��ȭ, �׷��� ������ ��Ȱ��ȭ
            heartImages[i].gameObject.SetActive(i <= _currentHp - 1);
            
        }
    }
    // Update is called once per frame

    public void RecoverHP(int healAmount)
    {
        //HPȸ��
        _currentHp += healAmount;

        _currentHp = Mathf.Clamp(_currentHp, 0, _maxHealth);

        UpdateHealthUI();
    }

}

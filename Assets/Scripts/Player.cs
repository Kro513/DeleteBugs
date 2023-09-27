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
            Debug.Log("게임 오버!");
            //게임 오버 UI 출력
            //게임오버 이펙트
            //사운드 출력
        }
        else
        {
            //타격 사운드 출력
            //타격 이펙트
        }


    }

    public void UpdateHealthUI()
    {
        //PlayerHPDisplay.text = "목숨:" + _currentHp.ToString() + "/" + _maxHealth.ToString();

        for (int i = 0; i <= 9; i++)
        {
            // i 값이 현재 체력보다 작을 때 하트 이미지를 활성화, 그렇지 않으면 비활성화
            heartImages[i].gameObject.SetActive(i <= _currentHp - 1);
            
        }
    }
    // Update is called once per frame

    public void RecoverHP(int healAmount)
    {
        //HP회복
        _currentHp += healAmount;

        _currentHp = Mathf.Clamp(_currentHp, 0, _maxHealth);

        UpdateHealthUI();
    }

}

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

    private void UpdateHealthUI()
    {
        PlayerHPDisplay.text = "목숨:" + _currentHp.ToString() + "/" + _maxHealth.ToString();
    }
    // Update is called once per frame

    public void RecoverHP()
    {
        //HP회복
    }
}

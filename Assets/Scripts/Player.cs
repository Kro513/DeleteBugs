using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    
    public int _Gold;
    public int _currentHp;
    public int maxHealth = 10;
    

    private void Awake()
    {
        _currentHp = maxHealth;
        _Gold = 100;
        
    }

    // Start is called before the first frame update
   public void GetDamage(int damage)
    {
        _currentHp -= damage;
        //UpdateHealthUI();

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

    // Update is called once per frame
    
}

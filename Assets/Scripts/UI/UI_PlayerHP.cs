using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHP : MonoBehaviour
{
    
    
    public Text PlayerHPDisplay;
    
    private Player _currentHp;
    private Player _maxHealth;
    
    private void Start()
    {
        UpdateHealthUI();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void UpdateHealthUI()
    {
        PlayerHPDisplay.text = "¸ñ¼û:" + _currentHp.ToString() + "/" + _maxHealth.ToString();
    }
}

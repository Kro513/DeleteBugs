using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStore : MonoBehaviour
{
    [SerializeField] private Button[] button;
    [SerializeField] private TowerSpawner towerSpawner;

    //[SerializeField] TMP_Text[] coinTxt;

    private void Start()
    {
        for(int i = 0; i < button.Length; i++)
        {
            int buttonIndex = i;
            button[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
    }

    void OnButtonClick(int buttonIndex)
    {
        //towerSpawner.SpawnTower(buttonIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStore : MonoBehaviour
{
    [SerializeField] private GameObject towerPerfab;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int price;
    [SerializeField] private TMP_Text priceTxt;

    public GameObject TowerPrefab
    {
        get
        {
            return towerPerfab;
        }
    }

    public Sprite Sprite
    {
        get
        {
            return sprite;
        }
    }

    public int Price
    {
        get
        {
            return price;
        }
    }

    private void Start()
    {
        priceTxt.text = price.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStore : MonoBehaviour
{
    [SerializeField] private GameObject towerPerfab;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int gold;
    [SerializeField] private TMP_Text goldTxt;

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

    public int Gold
    {
        get
        {
            return gold;
        }
    }

    private void Start()
    {
        goldTxt.text = gold.ToString();
    }
}

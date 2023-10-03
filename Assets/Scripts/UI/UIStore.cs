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
}

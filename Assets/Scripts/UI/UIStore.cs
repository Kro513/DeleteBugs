using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStore : MonoBehaviour
{
    [SerializeField] private GameObject towerPerfab;

    public GameObject TowerPrefab
    {
        get
        {
            return towerPerfab;
        }
    }
}

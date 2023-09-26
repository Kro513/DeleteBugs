using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsBulidTower { set; get; }

    private void Awake()
    {
        IsBulidTower = false;
    }
}

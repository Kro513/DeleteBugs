using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgrade : MonoBehaviour
{
    [SerializeField] private GameObject Upgrade1Img;
    [SerializeField] private GameObject Upgrade2Img;

    private void Awake()
    {
        OffUpgradeImg();
    }

    public  void OnUpgradeImg()
    {
        Upgrade1Img.gameObject.SetActive(true);
        Upgrade2Img.gameObject.SetActive(true);
    }

    public void OffUpgradeImg()
    {
        Upgrade1Img.gameObject.SetActive(false);
        Upgrade2Img.gameObject.SetActive(false);
    }
}

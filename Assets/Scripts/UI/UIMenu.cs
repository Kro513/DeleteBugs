using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private Button StoreBtn;
    [SerializeField] private Button UpgradeBtn;

    private void Start()
    {
        StoreBtn.onClick.AddListener(OpenStore);
        UpgradeBtn.onClick.AddListener(OpenUpgrade);
    }


    void OpenStore()
    {
        UIManager.Instance.uIUpgrade.gameObject.SetActive(false);
        UIManager.Instance.uIStore.gameObject.SetActive(true);
    }
   
    
    void OpenUpgrade()
    {
        UIManager.Instance.uIStore.gameObject.SetActive(false);
        UIManager.Instance.uIUpgrade.gameObject.SetActive(true);
    }
   
}



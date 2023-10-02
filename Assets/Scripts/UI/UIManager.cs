using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public UIMenu uIMenu { get; private set; }
    [field: SerializeField] public UIStore uIStore { get; private set; }
    [field: SerializeField] public UIUpgrade uIUpgrade { get; private set; }


    [field: SerializeField] public UIDayNight uiDayNight { get; private set; }

    public UIStore ClickedBtn { get; private set; }


    public static UIManager Instance;

    void Start()
    {
        Instance = this;
    }

    // Ÿ�� ����(Buy ��ư)
    public void PickTower(UIStore towerBtn)
    {
        this.ClickedBtn = towerBtn;
        Hover_.Instance.Activate(towerBtn.Sprite);
    }

    // Ÿ�� ����(Buy ��ư)
    public void BuyTower()
    {
        ClickedBtn = null;
    }
}

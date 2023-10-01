using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [field: SerializeField] public UIMenu uIMenu { get; private set; }
    [field: SerializeField] public UIStore uIStore { get; private set; }
    [field: SerializeField] public UIUpgrade uIUpgrade { get; private set; }

    public UIStore ClickedBtn { get; private set; }

    public static UIManager Instance;

    void Start()
    {
        Instance = this;
    }

    // 타워 선택(Buy 버튼)
    public void PickTower(UIStore towerBtn)
    {
        this.ClickedBtn = towerBtn;
        Hover_.Instance.Activate(towerBtn.Sprite);
    }

    // 타워 구매(Buy 버튼)
    public void BuyTower()
    {
        ClickedBtn = null;
    }
}

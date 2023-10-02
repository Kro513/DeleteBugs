using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [field: SerializeField] public UIMenu uIMenu { get; private set; }
    [field: SerializeField] public UIStore uIStore { get; private set; }
    [field: SerializeField] public UIUpgrade uIUpgrade { get; private set; }

    public UIStore ClickedBtn { get; set; }
    
    private Tower selectedTower; // The current selected tower

    public void PickTower(UIStore towerBtn)
    {
        this.ClickedBtn = towerBtn;
        Hover_.Instance.Activate(towerBtn.Sprite);
    }

    public void BuyTower()
    {
        ClickedBtn = null;
    }

}

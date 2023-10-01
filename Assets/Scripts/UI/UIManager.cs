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
    public void SelectTower(Tower tower)
    {
       /* if (selectedTower != null) // 하나의 타워만 범위 선택 가능
        {
            selectedTower.Select();
        }*/

        selectedTower = tower;
        selectedTower.Select();

    }
    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }
        selectedTower = null;
    }
}

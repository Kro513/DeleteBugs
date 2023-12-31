using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [field: SerializeField] public UIMenu uIMenu { get; private set; }
    [field: SerializeField] public UIStore uIStore { get; private set; }
    [field: SerializeField] public UIUpgrade uIUpgrade { get; private set; }
    [field: SerializeField] public UIDayNight uiDayNight { get; private set; }

    public UIStore ClickedBtn { get; set; }
    public AudioClip clip;

    private int gold;
    [SerializeField] private Text goldTxt;

    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            this.gold = value;
            this.goldTxt.text = value.ToString();
        }
    }
    void Start()
    {
        Gold = 100;
    }

    public void PickTower(UIStore towerBtn)
    {
        if (Gold >= towerBtn.Price)
        {
            this.ClickedBtn = towerBtn;
            Hover_.Instance.Activate(towerBtn.Sprite);
        }
        
    }

    public void BuyTower()
    {
        //ClickedBtn = null;
        if (Gold >= ClickedBtn.Price)
        {
            Gold -= ClickedBtn.Price;
            Hover_.Instance.Deactivate();
            SoundManager.instance.SFXPlay("Coin", clip);

        }

    }

    public void GetGold(int goldAmount)
    {
        Gold += goldAmount;
    }

}

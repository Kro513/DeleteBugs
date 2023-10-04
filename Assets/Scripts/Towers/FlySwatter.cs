using System;
using UnityEngine;

public class FlySwatter : TowerWeapon
{
	private bool isUpgraded = false;
	[SerializeField] private GameObject upgradeBtn;

	public void Start()
	{
		upgradeBtn.SetActive(false);

		if (!isUpgraded)
        {
			attackRate = 1;
			attackRange = 1f;
			attackDamage = 1;
		}
	}
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
			upgradeBtn.SetActive(true);
        }
    }

    public void Upgrade()
    {
		Btn();
    }

	private void Btn()
    {
		Debug.Log("¾÷");
		attackDamage += 2;
		attackRange += 3f;
		//UIManager.Instance.Gold
	}
}

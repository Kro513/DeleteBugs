using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class FlySwatter : TowerWeapon
{
	public void Start()
	{
		attackRate = 3;
		attackRange = 3f;
		attackDamage = 3;
	}
}
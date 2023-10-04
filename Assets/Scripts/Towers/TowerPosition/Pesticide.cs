using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class Pesticider : TowerWeapon
{
	public void Start()
	{
		attackRate = 1;
		attackRange = 1;
		attackDamage = 1;
	}
}
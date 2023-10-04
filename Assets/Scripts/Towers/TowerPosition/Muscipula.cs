using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


public class Muscipula : TowerWeapon
{
	public void Start()
	{
		attackRate = 5f;
		attackRange = 5;
		attackDamage = 5;
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class FlySwatterController : MonoBehaviour
{
	public event Action<Vector2> OnAttackEvent;

	private float _timeSinceLastAttack = float.MaxValue;
	protected bool IsAttacking { get; set; }

	protected virtual void Awake()
	{
		//Stats = GetComponent<Stats>();
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class FlySwatterController : MonoBehaviour
{
	public event Action<FlySwatterSO> OnAttackEvent;

	private float _timeSinceLastAttack = float.MaxValue;
	protected bool IsAttacking { get; set; }

	protected FlySwatter Stats { get; private set; }

	protected virtual void Awake()
	{
		Stats = GetComponent<FlySwatter>();
	}

	protected virtual void Update()
	{
		HandleAttackDelay();
	}

	public void HandleAttackDelay()
	{
		Debug.Log("1");
		CallAttackEvent(Stats.flySwatterStats.attackSO);
	}

	public void CallAttackEvent(FlySwatterSO attackSO)
	{
		Debug.Log("3");
		OnAttackEvent.Invoke(attackSO);
	}
}

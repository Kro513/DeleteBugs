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

	private void HandleAttackDelay()
	{
		if (Stats.flySwatterStats.attackSO == null)
			return;


		if (_timeSinceLastAttack <= Stats.flySwatterStats.attackSO.delay)
		{
			_timeSinceLastAttack += Time.deltaTime;
		}

		if (IsAttacking && _timeSinceLastAttack > Stats.flySwatterStats.attackSO.delay)
		{
			_timeSinceLastAttack = 0;
			CallAttackEvent(Stats.flySwatterStats.attackSO);
		}
	}

	private void CallAttackEvent(FlySwatterSO attackSO)
	{
		OnAttackEvent?.Invoke(attackSO);
	}
}

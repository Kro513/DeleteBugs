using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class MuscipulaController : MonoBehaviour
{
	public event Action<MuscipulaSO> OnAttackEvent;

	private float _timeSinceLastAttack = float.MaxValue;
	protected bool IsAttacking { get; set; }

	protected Muscipula Stats { get; private set; }

	protected virtual void Awake()
	{
		Stats = GetComponent<Muscipula>();
	}

	protected virtual void Update()
	{
		HandleAttackDelay();
	}

	private void HandleAttackDelay()
	{
		if (Stats.muscipulaStats.attackSO == null)
			return;


		if (_timeSinceLastAttack <= Stats.muscipulaStats.attackSO.delay)
		{
			_timeSinceLastAttack += Time.deltaTime;
		}

		if (IsAttacking && _timeSinceLastAttack > Stats.muscipulaStats.attackSO.delay)
		{
			_timeSinceLastAttack = 0;
			CallAttackEvent(Stats.muscipulaStats.attackSO);
		}
	}

	private void CallAttackEvent(MuscipulaSO attackSO)
	{
		OnAttackEvent?.Invoke(attackSO);
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public enum MuscipulaState
{
	SearchTarget = 0,
	AttackToTarget
}

public class Muscipula : MonoBehaviour
{
	[SerializeField] private MuscipulaStats baseStats;
	public MuscipulaStats muscipulaStats { get; private set; }
	public List<MuscipulaStats> statsModifiers = new List<MuscipulaStats>();

	private const float MinAttackRange = 0.5f;                                  // ��Ÿ�
	private const float MinAttackSpeed = 1.0f;                                  // ���� �ӵ� 
	private const float MinAttackDelay = 1.5f;                                  // ���� ������
	private const float MinAttackPower = 2.0f;                                  // ���ݷ�
	private const float MinAttackSize = 1.0f;                                   // ���� ����
	private SpriteRenderer characterRenderer;

	private MuscipulaState muscipulaState = MuscipulaState.SearchTarget; // �ĸ�ä ���� ����
	private Transform attackTarget = null;                                  // ���� ���
																			//private EnemySpawner enemySpawner;                                         // ���ӿ� �����ϴ� �� ���� ȹ���

	//  public void Setup(EnemySpawner enemySpawner)
	//  {
	//      this.enemySpawner = enemySpawner;

	//// ���� ���� MuscipulaState.SearchTarget���� ����
	//ChangeState(MuscipulaState.SearchTarget);
	//  }

	private void Awake()
	{
		UpdateMuscipulaStats();
	}

	public void AddStatModifier(MuscipulaStats statModifier)
	{
		statsModifiers.Add(statModifier);
		UpdateMuscipulaStats();
	}

	public void RemoveStatModifier(MuscipulaStats statModifier)
	{
		statsModifiers.Remove(statModifier);
		UpdateMuscipulaStats();
	}

	private void UpdateMuscipulaStats()
	{
		MuscipulaSO attackSO = null;
		if (baseStats.attackSO != null)
		{
			attackSO = Instantiate(baseStats.attackSO);
		}

		muscipulaStats = new MuscipulaStats { attackSO = attackSO };

		UpdateStats((a, b) => b, baseStats);
		if (muscipulaStats.attackSO != null)
		{
			muscipulaStats.attackSO.target = baseStats.attackSO.target;
		}

		foreach (MuscipulaStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
		{
			if (modifier.statsChangeType == MuscipulaStatsChangeType.Override)
			{
				UpdateStats((o, o1) => o1, modifier);
			}
			else if (modifier.statsChangeType == MuscipulaStatsChangeType.Add)
			{
				UpdateStats((o, o1) => o + o1, modifier);
			}
			else if (modifier.statsChangeType == MuscipulaStatsChangeType.Multiple)
			{
				UpdateStats((o, o1) => o * o1, modifier);
			}
		}

		LimitAllStats();
	}

	private void UpdateStats(Func<float, float, float> operation, MuscipulaStats newModifier)
	{
		if (muscipulaStats.attackSO == null || newModifier.attackSO == null)
			return;

		UpdateAttackStats(operation, muscipulaStats.attackSO, newModifier.attackSO);

		if (muscipulaStats.attackSO.GetType() != newModifier.attackSO.GetType())
		{
			return;
		}
	}

	private void UpdateAttackStats(Func<float, float, float> operation, MuscipulaSO currentAttack, MuscipulaSO newAttack)
	{
		if (currentAttack == null || newAttack == null)
		{
			return;
		}

		currentAttack.delay = operation(currentAttack.delay, newAttack.delay);
		currentAttack.power = operation(currentAttack.power, newAttack.power);
		currentAttack.size = operation(currentAttack.size, newAttack.size);
		currentAttack.speed = operation(currentAttack.speed, newAttack.speed);
		currentAttack.range = operation(currentAttack.range, newAttack.range);
	}

	private void LimitStats(ref float stat, float minVal)
	{
		stat = Mathf.Max(stat, minVal);
	}

	private void LimitAllStats()
	{
		if (muscipulaStats == null || muscipulaStats.attackSO == null)
		{
			return;
		}

		LimitStats(ref muscipulaStats.attackSO.delay, MinAttackDelay);
		LimitStats(ref muscipulaStats.attackSO.power, MinAttackPower);
		LimitStats(ref muscipulaStats.attackSO.size, MinAttackSize);
		LimitStats(ref muscipulaStats.attackSO.speed, MinAttackSpeed);
		LimitStats(ref muscipulaStats.attackSO.range, MinAttackRange);
	}

	private void ChangeState(MuscipulaState newState)
	{
		// ���� ���� ����
		StopCoroutine(muscipulaState.ToString());
		// ���� ����
		muscipulaState = newState;
		// ���ο� ���� ���
		StartCoroutine(muscipulaState.ToString());
	}

	private void Update()
	{
		if (attackTarget != null)
		{
			RotateToTarget();
		}
	}

	private void RotateToTarget()
	{
		float dx = attackTarget.position.x - transform.position.x;
		float dy = attackTarget.position.y - transform.position.y;

		float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
		characterRenderer.flipX = Mathf.Abs(degree) > 90f;
		transform.rotation = Quaternion.Euler(0, 0, degree);
	}

	//private IEnumerator SearchTarget()
	//{
	//    while (true)
	//    {
	//        // ���� ������ �ִ� ���� ã�� ���� ���� �Ÿ��� �ִ��� ũ�� ����
	//        float closestDistSqr = Mathf.Infinity;
	//        // EnemySpawner�� EnemyList�� �ִ� ���� �ʿ� �����ϴ� ��� �� �˻�
	//        for (int i = 0; i < enemySpawner.EnemyList.Count; i++)
	//        {
	//            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, attackTarget.position);
	//            // ���� �˻����� ������ �Ÿ��� ���ݹ��� ���� �ְ�, ������� �˻��� ������ �Ÿ��� �����ٸ�
	//            if ( distance <= attackRange && distance <= closestDistSqr )
	//            {
	//                closestDistSqr = distance;
	//                attackTarget = enemySpawner.EnemyList[i].transform;
	//            }
	//        }

	//        if (attackTarget != null)
	//        {
	//            ChangeState(MuscipulaState.AttackToTarget);
	//        }

	//        yield return null;
	//    }
	//}

	private IEnumerator AttackToTarget()
	{
		while (true)
		{
			// 1. target�� �ִ��� �˻� (�ٸ� Ÿ���� ���� ���, goal���� �̵��� ���� ��)
			if (attackTarget == null)
			{
				ChangeState(MuscipulaState.SearchTarget);
				break;
			}

			// 2. target�� ���� ���� �ȿ� �ִ��� �˻� (���� ���� ����� ���ο� �� Ž��)
			float distance = Vector3.Distance(attackTarget.position, transform.position);
			if (distance > MinAttackRange)
			{
				attackTarget = null;
				ChangeState(MuscipulaState.AttackToTarget);
				break;
			}

			// 3. attackRate �ð���ŭ ���
			yield return new WaitForSeconds(MinAttackRange);

			// 4. ���� (���ݹ����� �����)
			MuscipulaAttack();
		}
	}

	private void MuscipulaAttack()
	{
		// TODO
		// 1. ���� �ִϸ��̼� ���
		// 2. �ش� ������ enemy�鿡�� ����� ����
	}
}

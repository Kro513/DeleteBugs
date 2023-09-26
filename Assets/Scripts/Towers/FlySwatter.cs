using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public enum FlySwatterState
{
    SearchTarget = 0,
    AttackToTarget
}

public class FlySwatter : MonoBehaviour
{
	[SerializeField] private FlySwatterStats baseStats;
	public FlySwatterStats flySwatterStats { get; private set; }
	public List<FlySwatterStats> statsModifiers = new List<FlySwatterStats>();

	private const float MinAttackRange = 0.5f;                                  // ��Ÿ�
	private const float MinAttackSpeed = 1.0f;                                  // ���� �ӵ� 
	private const float MinAttackDelay = 1.5f;									// ���� ������
	private const float MinAttackPower = 2.0f;                                  // ���ݷ�
	private const float MinAttackSize = 1.0f;									// ���� ����
	private SpriteRenderer characterRenderer;

	private FlySwatterState flySwatterState = FlySwatterState.SearchTarget; // �ĸ�ä ���� ����
    private Transform attackTarget = null;                                  // ���� ���
																			//private EnemySpawner enemySpawner;                                         // ���ӿ� �����ϴ� �� ���� ȹ���

	//  public void Setup(EnemySpawner enemySpawner)
	//  {
	//      this.enemySpawner = enemySpawner;

	//// ���� ���� FlySwatterState.SearchTarget���� ����
	//ChangeState(FlySwatterState.SearchTarget);
	//  }

	private void Awake()
	{
		UpdateFlySwatterStats();
	}

	public void AddStatModifier(FlySwatterStats statModifier)
	{
		statsModifiers.Add(statModifier);
		UpdateFlySwatterStats();
	}

	public void RemoveStatModifier(FlySwatterStats statModifier)
	{
		statsModifiers.Remove(statModifier);
		UpdateFlySwatterStats();
	}

	private void UpdateFlySwatterStats()
	{
		FlySwatterSO attackSO = null;
		if (baseStats.attackSO != null)
		{
			attackSO = Instantiate(baseStats.attackSO);
		}

		flySwatterStats = new FlySwatterStats { attackSO = attackSO };

		UpdateStats((a, b) => b, baseStats);
		if (flySwatterStats.attackSO != null)
		{
			flySwatterStats.attackSO.target = baseStats.attackSO.target;
		}

		foreach (FlySwatterStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
		{
			if (modifier.statsChangeType == FlySwatterStatsChangeType.Override)
			{
				UpdateStats((o, o1) => o1, modifier);
			}
			else if (modifier.statsChangeType == FlySwatterStatsChangeType.Add)
			{
				UpdateStats((o, o1) => o + o1, modifier);
			}
			else if (modifier.statsChangeType == FlySwatterStatsChangeType.Multiple)
			{
				UpdateStats((o, o1) => o * o1, modifier);
			}
		}

		LimitAllStats();
	}

	private void UpdateStats(Func<float, float, float> operation, FlySwatterStats newModifier)
	{
		if (flySwatterStats.attackSO == null || newModifier.attackSO == null)
			return;

		UpdateAttackStats(operation, flySwatterStats.attackSO, newModifier.attackSO);

		if (flySwatterStats.attackSO.GetType() != newModifier.attackSO.GetType())
		{
			return;
		}
	}

	private void UpdateAttackStats(Func<float, float, float> operation, FlySwatterSO currentAttack, FlySwatterSO newAttack)
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
		if (flySwatterStats == null || flySwatterStats.attackSO == null)
		{
			return;
		}

		LimitStats(ref flySwatterStats.attackSO.delay, MinAttackDelay);
		LimitStats(ref flySwatterStats.attackSO.power, MinAttackPower);
		LimitStats(ref flySwatterStats.attackSO.size, MinAttackSize);
		LimitStats(ref flySwatterStats.attackSO.speed, MinAttackSpeed);
		LimitStats(ref flySwatterStats.attackSO.range, MinAttackRange);
	}

	private void ChangeState(FlySwatterState newState)
	{
		// ���� ���� ����
        StopCoroutine(flySwatterState.ToString());
        // ���� ����
        flySwatterState = newState;
        // ���ο� ���� ���
        StartCoroutine(flySwatterState.ToString());
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
    //            ChangeState(FlySwatterState.AttackToTarget);
    //        }

    //        yield return null;
    //    }
    //}

    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            // 1. target�� �ִ��� �˻� (�ٸ� Ÿ���� ���� ���, goal���� �̵��� ���� ��)
            if ( attackTarget == null)
            {
                ChangeState(FlySwatterState.SearchTarget);
                break;
            }

            // 2. target�� ���� ���� �ȿ� �ִ��� �˻� (���� ���� ����� ���ο� �� Ž��)
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if(distance > MinAttackRange)
            {
                attackTarget = null;
                ChangeState(FlySwatterState.AttackToTarget);
                break;
            }

            // 3. attackRate �ð���ŭ ���
            yield return new WaitForSeconds(MinAttackRange);

            // 4. ���� (���ݹ����� �����)
            FlySwatterAttack();
		}
    }

	private void FlySwatterAttack()
	{
		// TODO
        // 1. ���� �ִϸ��̼� ���
        // 2. �ش� ������ enemy�鿡�� ����� ����
	}
}

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

	private const float MinAttackRange = 0.5f;                                  // 사거리
	private const float MinAttackSpeed = 1.0f;                                  // 공격 속도 
	private const float MinAttackDelay = 1.5f;                                  // 공격 딜레이
	private const float MinAttackPower = 2.0f;                                  // 공격력
	private const float MinAttackSize = 1.0f;                                   // 공격 범위
	private SpriteRenderer characterRenderer;

	private MuscipulaState muscipulaState = MuscipulaState.SearchTarget; // 파리채 무기 상태
	private Transform attackTarget = null;                                  // 공격 대상
																			//private EnemySpawner enemySpawner;                                         // 게임에 존재하는 적 정보 획득용

	//  public void Setup(EnemySpawner enemySpawner)
	//  {
	//      this.enemySpawner = enemySpawner;

	//// 최초 상태 MuscipulaState.SearchTarget으로 설정
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
		// 이전 상태 종료
		StopCoroutine(muscipulaState.ToString());
		// 상태 변경
		muscipulaState = newState;
		// 새로운 상태 재생
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
	//        // 제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
	//        float closestDistSqr = Mathf.Infinity;
	//        // EnemySpawner의 EnemyList에 있는 현재 맵에 존재하는 모든 적 검사
	//        for (int i = 0; i < enemySpawner.EnemyList.Count; i++)
	//        {
	//            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, attackTarget.position);
	//            // 현재 검사중인 적과의 거리가 공격범위 내에 있고, 현재까지 검사한 적보다 거리가 가깝다면
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
			// 1. target이 있는지 검사 (다른 타워에 의해 사망, goal까지 이동해 삭제 등)
			if (attackTarget == null)
			{
				ChangeState(MuscipulaState.SearchTarget);
				break;
			}

			// 2. target이 공격 범위 안에 있는지 검사 (공격 범위 벗어나면 새로운 적 탐색)
			float distance = Vector3.Distance(attackTarget.position, transform.position);
			if (distance > MinAttackRange)
			{
				attackTarget = null;
				ChangeState(MuscipulaState.AttackToTarget);
				break;
			}

			// 3. attackRate 시간만큼 대기
			yield return new WaitForSeconds(MinAttackRange);

			// 4. 공격 (공격범위에 대미지)
			MuscipulaAttack();
		}
	}

	private void MuscipulaAttack()
	{
		// TODO
		// 1. 공격 애니메이션 재생
		// 2. 해당 범위의 enemy들에게 대미지 전달
	}
}

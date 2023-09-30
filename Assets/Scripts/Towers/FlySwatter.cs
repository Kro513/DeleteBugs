using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
	Animator animator;

	private const float MinAttackRange = 0.5f;                                  // 사거리
	private const float MinAttackSpeed = 1.0f;                                  // 공격 속도 
	private const float MinAttackDelay = 1.5f;									// 공격 딜레이
	private const float MinAttackPower = 2.0f;                                  // 공격력
	private const float MinAttackSize = 1.0f;									// 공격 범위
	private SpriteRenderer characterRenderer;

	private FlySwatterState flySwatterState = FlySwatterState.SearchTarget; // 파리채 무기 상태
    private Transform attackTarget = null;                                  // 공격 대상

	private float curTime;
	public Transform pos;
	public float circleSize;
	//private EnemySpawner enemySpawner;                                         // 게임에 존재하는 적 정보 획득용

	//  public void Setup(EnemySpawner enemySpawner)
	//  {
	//      this.enemySpawner = enemySpawner;

	//// 최초 상태 FlySwatterState.SearchTarget으로 설정
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
		// 이전 상태 종료
        StopCoroutine(flySwatterState.ToString());
        // 상태 변경
        flySwatterState = newState;
        // 새로운 상태 재생
        StartCoroutine(flySwatterState.ToString());
	}

	private void Update()
	{
		if(curTime <= 0)
		{
			if(attackTarget != null)
			{
				RotateToTarget();

				Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(pos.position, circleSize, 0);
				foreach (Collider2D collider in collider2Ds)
				{
					Debug.Log(collider.tag);
				}
				animator.SetTrigger("atk");
				curTime = MinAttackDelay;
			}
		}
		else
		{
			curTime -= Time.deltaTime;
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(pos.position, circleSize);
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
    //            ChangeState(FlySwatterState.AttackToTarget);
    //        }

    //        yield return null;
    //    }
    //}

    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            // 1. target이 있는지 검사 (다른 타워에 의해 사망, goal까지 이동해 삭제 등)
            if ( attackTarget == null)
            {
                ChangeState(FlySwatterState.SearchTarget);
                break;
            }

            // 2. target이 공격 범위 안에 있는지 검사 (공격 범위 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if(distance > MinAttackRange)
            {
                attackTarget = null;
                ChangeState(FlySwatterState.AttackToTarget);
                break;
            }

            // 3. attackRate 시간만큼 대기
            yield return new WaitForSeconds(MinAttackRange);

            // 4. 공격 (공격범위에 대미지)
            FlySwatterAttack();
		}
    }

	private void FlySwatterAttack()
	{
		// TODO
        // 1. 공격 애니메이션 재생
        // 2. 해당 범위의 enemy들에게 대미지 전달
	}
}

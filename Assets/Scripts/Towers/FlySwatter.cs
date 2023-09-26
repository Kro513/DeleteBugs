using System;
using System.Collections;
using System.Collections.Generic;
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

	private const float attackRange = 0.5f;                                 // 사거리
	private const float attackRate = 1.0f;                                  // 공격 속도 
	private SpriteRenderer characterRenderer;

	private FlySwatterState flySwatterState = FlySwatterState.SearchTarget; // 파리채 무기 상태
    private Transform attackTarget = null;                                  // 공격 대상
    //private EnemySpawner enemySpawner;                                         // 게임에 존재하는 적 정보 획득용

  //  public void Setup(EnemySpawner enemySpawner)
  //  {
  //      this.enemySpawner = enemySpawner;

		//// 최초 상태 FlySwatterState.SearchTarget으로 설정
		//ChangeState(FlySwatterState.SearchTarget);
  //  }

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
            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(FlySwatterState.AttackToTarget);
                break;
            }

            // 3. attackRate 시간만큼 대기
            yield return new WaitForSeconds(attackRange);

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

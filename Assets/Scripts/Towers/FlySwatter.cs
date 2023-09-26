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

	private const float attackRange = 0.5f;                                 // ��Ÿ�
	private const float attackRate = 1.0f;                                  // ���� �ӵ� 
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
            if(distance > attackRange)
            {
                attackTarget = null;
                ChangeState(FlySwatterState.AttackToTarget);
                break;
            }

            // 3. attackRate �ð���ŭ ���
            yield return new WaitForSeconds(attackRange);

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

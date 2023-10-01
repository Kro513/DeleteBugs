using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

// 타워 상태 정의
public enum PesticideState
{
    SearchTarget = 0,
    AttackToTarget
}
public class Pesticide : MonoBehaviour
{
    [SerializeField] private PesticideStats baseStats; // 기본 스탯
    public PesticideStats pesticideStats { get; private set; } // 현재 스탯(변경될 때 업데이트 됨.)
    public List<PesticideStats> statsModifiers = new List<PesticideStats>(); // 스탯 변경자 목록
    Animator animator;

    private const float MinAttackRange = 0.5f;                                  // 사거리
    private const float MinAttackSpeed = 1.0f;                                  // 공격 속도 
    private const float MinAttackDelay = 1.5f;                                  // 공격 딜레이
    private const float MinAttackPower = 2.0f;                                  // 공격력
    private const float MinAttackSize = 1.0f;                                   // 공격 범위
    private SpriteRenderer characterRenderer;

    private PesticideState pesticideState = PesticideState.SearchTarget; // 살충제 무기 상태
    private Transform attackTarget = null;                               // 공격 대상

    private float curTime; // 다음 공격까지 대기 시간 추적
    public Transform pos; // 타워 위치
    public float circleSize; // 공격범위

    private void Awake()
    {
        UpdatePesticideStats();
    }

    public void AddStateModifier(PesticideStats statModifier) // 스탯 변경자 추가
    {
        statsModifiers.Add(statModifier);
    }
    public void RemoveStatModifier(PesticideStats statModifier) // 스탯 변경자 제거
    {
        statsModifiers.Remove(statModifier);
    }
    private void UpdatePesticideStats() 
    {
        PesticideSO attackSO = null;
        if(baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }
        
        pesticideStats = new PesticideStats { attackSO = attackSO };

        UpdateStats((a, b) => b, baseStats);
        if(pesticideStats.attackSO != null)
        {
            pesticideStats.attackSO.target = baseStats.attackSO.target;
        }

        foreach(PesticideStats modifier in statsModifiers.OrderBy(o => o.statsChangeType))
        {
            if(modifier.statsChangeType == PesticideStatsChangeType.Override)
            {
                UpdateStats((o, o1) => o1, modifier);
            }
            else if(modifier.statsChangeType == PesticideStatsChangeType.Add)
            {
                UpdateStats((o, o1) => o + o1, modifier);
            }
            else if (modifier.statsChangeType == PesticideStatsChangeType.Multiple)
            {
                UpdateStats((o, o1) => o * o1, modifier);
            }
        }
        LimitAllStats();
    }
    private void UpdateStats(Func<float, float, float> operation, PesticideStats newModifier) 
    {
        if (pesticideStats.attackSO == null || newModifier.attackSO == null)
            return;

        UpdateAttackStats(operation, pesticideStats.attackSO, newModifier.attackSO);

        if(pesticideStats.attackSO.GetType() != newModifier.attackSO.GetType())
        {
            return;
        }
    }

    private void UpdateAttackStats(Func<float, float, float> operation, PesticideSO currentAttack, PesticideSO newAttack)
    {
        if(currentAttack == null || newAttack == null)
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
        if(pesticideStats == null || pesticideStats.attackSO == null)
        {
            return;
        }
        LimitStats(ref pesticideStats.attackSO.delay, MinAttackDelay);
        LimitStats(ref pesticideStats.attackSO.power, MinAttackPower);
        LimitStats(ref pesticideStats.attackSO.size, MinAttackSize);
        LimitStats(ref pesticideStats.attackSO.speed, MinAttackSpeed);
        LimitStats(ref pesticideStats.attackSO.range, MinAttackRange);
    }
    private void ChangeState(PesticideState newState)
    {
        StopCoroutine(pesticideState.ToString()); // 이전 상태 종료

        pesticideState = newState; // 상태 변경

        StopCoroutine(pesticideState.ToString()); // 새로운 상태 재생
    }

    private void Update()
    {
        if(curTime <= 0)
        {
            if(attackTarget != null)
            {
                Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(pos.position, circleSize, 0);
                foreach(Collider2D collider in collider2Ds)
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

    // 타워 회전
    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x; // 타워와 적 사이의 수평 거리
        float dy = attackTarget.position.y - transform.position.y; // 타원와 적 사이의 수직 거리

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg; // 라디안 단위로 표현된 각도 계산
        characterRenderer.flipX = Mathf.Abs(degree) > 90f; // 타워 좌우로 뒤집기
        transform.rotation = Quaternion.Euler(0, 0, degree); // z축을 중심으로 degree만큼 회전 설정
    }

    // 타원 공격 루프
    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            // 1. target이 있는지 검사
            if(attackTarget == null)
            {
                ChangeState(PesticideState.SearchTarget);
                break;
            }

            // 2. target이 공격 범위 안에 있는지 검사
            float distance = Vector3.Distance(attackTarget.position, transform.position); // 타워와 적 사이 거리
            if(distance > MinAttackRange)
            {
                attackTarget = null;
                ChangeState(PesticideState.AttackToTarget);
                break;
            }

            // 3. attackRate 시간만큼 대기
            yield return new WaitForSeconds(MinAttackRange); // 공격 대기 시간: 공격속도 

            // 4. 공격
            PesticideAttack();
        }
    }
    private void PesticideAttack()
    {

    }
}

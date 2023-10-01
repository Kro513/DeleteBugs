using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

// Ÿ�� ���� ����
public enum PesticideState
{
    SearchTarget = 0,
    AttackToTarget
}
public class Pesticide : MonoBehaviour
{
    [SerializeField] private PesticideStats baseStats; // �⺻ ����
    public PesticideStats pesticideStats { get; private set; } // ���� ����(����� �� ������Ʈ ��.)
    public List<PesticideStats> statsModifiers = new List<PesticideStats>(); // ���� ������ ���
    Animator animator;

    private const float MinAttackRange = 0.5f;                                  // ��Ÿ�
    private const float MinAttackSpeed = 1.0f;                                  // ���� �ӵ� 
    private const float MinAttackDelay = 1.5f;                                  // ���� ������
    private const float MinAttackPower = 2.0f;                                  // ���ݷ�
    private const float MinAttackSize = 1.0f;                                   // ���� ����
    private SpriteRenderer characterRenderer;

    private PesticideState pesticideState = PesticideState.SearchTarget; // ������ ���� ����
    private Transform attackTarget = null;                               // ���� ���

    private float curTime; // ���� ���ݱ��� ��� �ð� ����
    public Transform pos; // Ÿ�� ��ġ
    public float circleSize; // ���ݹ���

    private void Awake()
    {
        UpdatePesticideStats();
    }

    public void AddStateModifier(PesticideStats statModifier) // ���� ������ �߰�
    {
        statsModifiers.Add(statModifier);
    }
    public void RemoveStatModifier(PesticideStats statModifier) // ���� ������ ����
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
        StopCoroutine(pesticideState.ToString()); // ���� ���� ����

        pesticideState = newState; // ���� ����

        StopCoroutine(pesticideState.ToString()); // ���ο� ���� ���
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

    // Ÿ�� ȸ��
    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x; // Ÿ���� �� ������ ���� �Ÿ�
        float dy = attackTarget.position.y - transform.position.y; // Ÿ���� �� ������ ���� �Ÿ�

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg; // ���� ������ ǥ���� ���� ���
        characterRenderer.flipX = Mathf.Abs(degree) > 90f; // Ÿ�� �¿�� ������
        transform.rotation = Quaternion.Euler(0, 0, degree); // z���� �߽����� degree��ŭ ȸ�� ����
    }

    // Ÿ�� ���� ����
    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            // 1. target�� �ִ��� �˻�
            if(attackTarget == null)
            {
                ChangeState(PesticideState.SearchTarget);
                break;
            }

            // 2. target�� ���� ���� �ȿ� �ִ��� �˻�
            float distance = Vector3.Distance(attackTarget.position, transform.position); // Ÿ���� �� ���� �Ÿ�
            if(distance > MinAttackRange)
            {
                attackTarget = null;
                ChangeState(PesticideState.AttackToTarget);
                break;
            }

            // 3. attackRate �ð���ŭ ���
            yield return new WaitForSeconds(MinAttackRange); // ���� ��� �ð�: ���ݼӵ� 

            // 4. ����
            PesticideAttack();
        }
    }
    private void PesticideAttack()
    {

    }
}

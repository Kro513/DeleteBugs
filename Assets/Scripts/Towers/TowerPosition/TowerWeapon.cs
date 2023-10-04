using System;
using System.Collections;
using UnityEngine;

public enum TowerWeaponState
{
    SearchTarget = 0,
    AttackToTarget
}

public class TowerWeapon : MonoBehaviour
{
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform spawnPoint;
    [SerializeField] protected float attackRate;
    [SerializeField] protected float attackRange;
    [SerializeField] protected int attackDamage;

	protected TowerWeaponState weaponState = TowerWeaponState.SearchTarget;
	protected Transform attackTarget = null;
	protected EnemySpawner enemySpawner;

	public event Action<FlySwatterSO> OnAttackEvent;

	public void Setup(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;

        ChangeState(TowerWeaponState.SearchTarget);
    }
    public void ChangeState(TowerWeaponState newState)
    {
        StopCoroutine(weaponState.ToString());
        weaponState = newState;
        StartCoroutine(weaponState.ToString());
    }

	private IEnumerator SearchTarget()
    {
        while (true)
        {
            float closestDistSqr = Mathf.Infinity;

            for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
            {
                float distacne = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);
                if (distacne <= attackRange && distacne <= closestDistSqr)
                {
                    closestDistSqr = distacne;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }
            if (attackTarget != null)
            {
                ChangeState(TowerWeaponState.AttackToTarget);
            }
            yield return null;
        }
    }

	private IEnumerator AttackToTarget()
    {
        while (true)
        {
            if (attackTarget == null)
            {
                ChangeState(TowerWeaponState.SearchTarget);
                break;
            }

            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if (distance > attackRange)
            {
                attackTarget = null;
                ChangeState(TowerWeaponState.SearchTarget);
                break;
            }

			yield return new WaitForSeconds(attackRate);

			SpawnProjectile();
        }
    }
    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<Projectile>().Setup(attackTarget, attackDamage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
    SearchTarget = 0,
    AttackToTarget
}

public class TowerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float attackRate = 0.5f;
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private int attackDamage = 1;

    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;

	[SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private SpriteRenderer projectilePoint;

	public void Setup(EnemySpawner enemySpawner)
    {
        this.enemySpawner = enemySpawner;

        ChangeState(WeaponState.SearchTarget);
    }
    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());
        weaponState = newState;
        StartCoroutine(weaponState.ToString());
    }
    private void Update()
    {
        if (attackTarget != null)
        {
			Vector2 direction = attackTarget.position - transform.position;
			RotateToTarget(direction);
		}
    }
    private void RotateToTarget(Vector2 direction)
    {
        
		float degree = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectilePoint.flipY = Mathf.Abs(degree) > 90f;
        characterRenderer.flipX = projectilePoint.flipY;
		transform.rotation = Quaternion.Euler(0, 0, degree);
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
                ChangeState(WeaponState.AttackToTarget);
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
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if (distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
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

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
    [SerializeField] private TowerTemplate towerTemplate; // 타워 정보
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPoint;

    /*[SerializeField] private float attackRate = 0.5f;
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private int attackDamage = 1;*/

    private int level = 0;
    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;

    public Sprite TowerSprite => towerTemplate.weapon[level].sprite;
    public float Damage => towerTemplate.weapon[level].damage;
    public float Rate => towerTemplate.weapon[level].rate;
    public float Range => towerTemplate.weapon[level].range;
    public int Level => level + 1;

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
                if (distacne <= towerTemplate.weapon[level].range && distacne <= closestDistSqr)
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
            if (distance > towerTemplate.weapon[level].range)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            yield return new WaitForSeconds(towerTemplate.weapon[level].rate);

            SpawnProjectile();
        }
    }
    private void SpawnProjectile()
    {
        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<Projectile>().Setup(attackTarget, towerTemplate.weapon[level].damage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected int wayPointCount;
    protected Transform[] wayPoints;
    protected int currentIndex = 0;
    protected Movement2D movement2D;
    private EnemySpawner enemySpawner;
    //[SerializeField]
    //private int gold = 10;


    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();
        this.enemySpawner = enemySpawner;

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while (true)
        {
            transform.Rotate(Vector3.forward * 0);
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }

    protected virtual void NextMoveTo()
    {
        if (currentIndex < wayPointCount - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);

            if (direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                angle -= 90.0f;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        else
        {
            if (gameObject.layer == LayerMask.NameToLayer("Boss"))
            {
                GameManager.Instance.player.GetDamage(10); // 보스 몬스터에게 10의 데미지
                                                           // 추가적인 보스 몬스터 동작 수행
            }
            GameManager.Instance.player.GetDamage(1);
            OnDie();

            //Destroy(gameObject);
        }
    }
    public void OnDie()
    {
        enemySpawner.DestoryEnemy(this);
    }
}
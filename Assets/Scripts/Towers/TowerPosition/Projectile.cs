using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Movement2D movement2D;
    private Transform target;

    public void Setup(Transform target)
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;
    }
    void Start()
    {
        movement2D = GetComponent<Movement2D>();
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return; // 적이 아닌 대상과 충돌
        if (collision.transform != target) return;

        collision.GetComponent<Enemy>().OnDie();
        Destroy(gameObject);
    }
}

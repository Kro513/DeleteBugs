using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Enemy target;

    private Queue<Enemy> enemies = new Queue<Enemy>();

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Attack();
        Debug.Log(target);
    }

    public void Select()
    {
        Debug.Log("click2");
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }
    private void Attack()
    {
        if(target == null && enemies.Count > 0)
        {
            target = enemies.Dequeue();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            enemies.Enqueue(collision.GetComponent<Enemy>());
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            target = null;
        }
    }
}

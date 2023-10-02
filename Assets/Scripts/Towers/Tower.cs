using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Enemy target;

    public Enemy Target
    {
        get { return target; }
    }

    private Queue<Enemy> enemies = new Queue<Enemy>();

    [SerializeField] private float attackCooldown;

    [SerializeField] private string projectileType;
    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Debug.Log(target);
    }

    public void Select()
    {
        Debug.Log("click2");
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
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

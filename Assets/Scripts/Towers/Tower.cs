using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;

    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {

    }

    public void Select()
    {

        //mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
        if (!mySpriteRenderer.enabled)
        {
            mySpriteRenderer.enabled = true;
        }
        else
        {
            mySpriteRenderer.enabled = false;
        }


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

    }
}

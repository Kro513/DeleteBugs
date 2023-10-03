using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySwatterAnimationController : FlySwatterAnimations
{
	private static readonly int Attack = Animator.StringToHash("Attack");

    protected override void Awake()
	{
        base.Awake();
	}


	// Start is called before the first frame update
	void Start()
    {
        controller.OnAttackEvent += Attacking;
    }

	public void Attacking(FlySwatterSO sO)
	{
		animator.SetTrigger(Attack);
	}
}

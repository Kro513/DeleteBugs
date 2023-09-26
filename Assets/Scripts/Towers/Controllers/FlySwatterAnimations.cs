using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySwatterAnimations : MonoBehaviour
{
	protected Animator animator;
	protected FlySwatterController controller;

	protected virtual void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		controller = GetComponent<FlySwatterController>();
	}
}

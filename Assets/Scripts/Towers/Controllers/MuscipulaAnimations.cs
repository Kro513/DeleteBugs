using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuscipulaAnimations : MonoBehaviour
{
	protected Animator animator;
	protected MuscipulaController controller;

	protected virtual void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		controller = GetComponent<MuscipulaController>();
	}
}

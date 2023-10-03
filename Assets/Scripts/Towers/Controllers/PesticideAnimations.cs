using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PesticideAnimations : MonoBehaviour
{
	protected Animator animator;
	protected PesticideController controller;

	protected virtual void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		controller = GetComponent<PesticideController>();
	}
}

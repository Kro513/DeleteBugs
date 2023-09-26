using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttackData", menuName = "FlySwatterController/Attack/Default", order = 0)]

public class FlySwatterSO : ScriptableObject
{
	[Header("Attack Info")]
	public float size;
	public float delay;
	public float power;
	public float speed;
	public float range;
	public LayerMask target;
}

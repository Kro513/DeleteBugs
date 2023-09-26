using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatsChangeType
{
	Add,
	Multiple,
	Override,
}

[Serializable]
public class FlySwatterStats
{
	public StatsChangeType statsChangeType;

	// 공격 데이터
	//public AttackSO attackSO;
}

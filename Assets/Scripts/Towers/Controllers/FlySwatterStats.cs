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

	// ���� ������
	//public AttackSO attackSO;
}

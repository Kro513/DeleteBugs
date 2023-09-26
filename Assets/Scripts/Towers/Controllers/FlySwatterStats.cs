using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlySwatterStatsChangeType
{
	Add,
	Multiple,
	Override,
}

[Serializable]
public class FlySwatterStats
{
	public FlySwatterStatsChangeType statsChangeType;

	// 공격 데이터
	public FlySwatterSO attackSO;
}

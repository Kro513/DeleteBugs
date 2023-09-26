using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MuscipulaStatsChangeType
{
	Add,
	Multiple,
	Override,
}

[Serializable]
public class MuscipulaStats
{
	public MuscipulaStatsChangeType statsChangeType;

	// 공격 데이터
	public MuscipulaSO attackSO;
}

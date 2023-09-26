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

	// ���� ������
	public MuscipulaSO attackSO;
}

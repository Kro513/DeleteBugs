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

	// ���� ������
	public FlySwatterSO attackSO;
}

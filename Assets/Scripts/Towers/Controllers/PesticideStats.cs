using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PesticideStatsChangeType
{
	Add,
	Multiple,
	Override,
}

[Serializable]
public class PesticideStats
{
	public PesticideStatsChangeType statsChangeType;


	// ���� ������
	public PesticideSO attackSO;
}

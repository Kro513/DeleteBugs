using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;
    [SerializeField]
    private EnemySpawner enemySpawner;
    private int currentwaveIndex = -1;

    public void StartWave()
    {
        if(enemySpawner.EnemyList.Count == 0 && currentwaveIndex <waves.Length-1)
        {
            currentwaveIndex ++;
            enemySpawner.StartWave(waves[currentwaveIndex]);
        }
    }

}

[System.Serializable]
public struct Wave
{
    public float spawmTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject {
    [SerializeField] GameObject pfEnemy;
    [SerializeField] GameObject pfPath;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() {
        return pfEnemy;
    }

    public List<Transform> GetWaypoints() {
        List<Transform> waveWeypoints = new List<Transform>();

        foreach (Transform child in pfPath.transform) {
            waveWeypoints.Add(child);
        }

        return waveWeypoints;
    }

    public float GetTimeBetweenSpawn() {
        return timeBetweenSpawns;
    }

    public float GetSpawnRandomFactor() {
        return spawnRandomFactor;
    }

    public int GetNumberOfEnemies() {
        return numberOfEnemies;
    }

    public float GetMoveSpeed() {
        return moveSpeed;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    [SerializeField] private GameObject enemy;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private float time = 2;
    private WaitForSeconds _timer;


    private void Start()
    {
        _timer = new WaitForSeconds(time);
        StartCoroutine(SpawnLoop());
    }


    private IEnumerator SpawnLoop()
    {
        while (player != null)
        {
            var randSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            var pos = randSpawnPoint.position;

            GameObject enemyObject = Instantiate(enemy, pos, Quaternion.Euler(Vector2.zero), transform);
            enemyObject.GetComponent<Enemy>().SetEnemyTarget(player);

            yield return _timer;
        }
    }


    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}

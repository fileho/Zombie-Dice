using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject normal;
    [SerializeField] private GameObject fast;
    [SerializeField] private GameObject ranged;
    [SerializeField] private GameObject tank;

    [SerializeField] private float radius;
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private TMP_Text text;


    public static EnemySpawner instance;

    private int wave = 1;

    private int enemyCount;

    private bool spawning;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Invoke(nameof(SpawnWave), 1f);
    }

    public int Wave()
    {
        return wave;
    }


    public void EnemyDeath()
    {
        enemyCount--;

        if (spawning)
            return;

        if (enemyCount < wave)
        {
            spawning = true;
            Invoke(nameof(SpawnWave), 15f);
        }
    }

    public void SpawnWave()
    {
        DiceSpawner.instance.SpawnWave(wave);
        StartCoroutine(ShowWave());

        enemyCount += NormalCount() * spawnPoints.Count;
        enemyCount += FastCount() * spawnPoints.Count;
        enemyCount += RangedCount() * spawnPoints.Count;
        enemyCount += TankCount() * spawnPoints.Count;

        StartCoroutine(SpawnEnemy(normal, NormalCount()));
        StartCoroutine(SpawnEnemy(fast, FastCount()));
        StartCoroutine(SpawnEnemy(ranged, RangedCount()));
        StartCoroutine(SpawnEnemy(tank, TankCount()));

        ++wave;
        spawning = false;
    }

    private int NormalCount()
    {
        return 2 + wave;
    }

    private int FastCount()
    {
        return wave / 2;
    }

    private int RangedCount()
    {
        return wave / 3;
    }

    private int TankCount()
    {
        return wave / 5;
    }

    private IEnumerator SpawnEnemy(GameObject prefab, int count)
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 pos = (Vector3)Random.insideUnitCircle * radius + spawnPoint.position;
                Instantiate(prefab, pos, Quaternion.identity);
                yield return new WaitForSeconds(2f);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        foreach (var item in spawnPoints)
        {
            Gizmos.DrawWireSphere(item.position, radius);
        }

    }

    private IEnumerator ShowWave()
    {
        text.enabled = true;
        text.text = "Wave " + wave;

        yield return new WaitForSeconds(2f);

        text.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy normal;
    [SerializeField] private Enemy fast;
    [SerializeField] private Enemy ranged;
    [SerializeField] private Enemy tank;

    [SerializeField] private float radius;

    private int wave = 1;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SpawnWave();
    }

    public void SpawnWave()
    {
        StartCoroutine(SpawnEnemy(normal, NormalCount()));
        StartCoroutine(SpawnEnemy(fast, FastCount()));
        StartCoroutine(SpawnEnemy(ranged, RangedCount()));
        StartCoroutine(SpawnEnemy(tank, TankCount()));

        ++wave;
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

    private IEnumerator SpawnEnemy(Enemy prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = (Vector3)Random.insideUnitCircle * radius + transform.position;
            Instantiate(prefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

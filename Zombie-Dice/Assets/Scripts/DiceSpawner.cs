using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private Cube dice;
    [SerializeField] private float radius;

    public static DiceSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 5f, 10f);
    }

    // Update is called once per frame
    public void SpawnWave(int waveCount)
    {
        SpawnCubes(waveCount + 2);
    }


    private void SpawnCubes(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = (Vector3)Random.insideUnitCircle * radius + transform.position;
            Instantiate(dice, pos, Quaternion.identity);
        }
    }

    private void SpawnCube()
    {
        SpawnCubes(1);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

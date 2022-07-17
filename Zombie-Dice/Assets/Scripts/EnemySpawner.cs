using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy prefab;

    [SerializeField] private float radius;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SpawnWave(5);
    }

    public void SpawnWave(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = (Vector3)Random.insideUnitCircle * radius + transform.position;
            Instantiate(prefab, pos, Quaternion.identity);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

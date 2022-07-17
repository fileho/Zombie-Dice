using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offset;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.position;
        pos.y += offset;
        transform.position = pos;
    }
}

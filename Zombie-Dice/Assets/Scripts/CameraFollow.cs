using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Character.instance.transform.position;
        pos.z = -100;
        transform.position = pos;
    }
}

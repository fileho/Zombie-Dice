using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private List<Attachment> attachments;

    private Transform cube;    
    private Quaternion rotation = Quaternion.Euler(0, 0, 0);
    private int rindex;

    // Start is called before the first frame update
    void Start()
    {
        cube = transform.GetChild(0);

        for (int i = 0; i < attachments.Count; i++)
        {
            cube.GetChild(i).GetComponent<SpriteRenderer>().sprite = attachments[i].icon;
        }

    }

    private void PickRotation()
    {
        // float x = Random.Range(0, 4);
        // float y = Random.Range(0, 4);
        // 
        // if (x == rot.x)
        //     x = (x + 1) % 4;
        // if (y == rot.y)
        //     y = (y + 1) % 4;

        List<(int, int)> rotations = new List<(int, int)> { (0, 0), (90, 90), (0, 180), (180, 270), (90, 0), (90, 180) };

        int i = Random.Range(0, 6);
        if (i == rindex)
            i = (i + 1) % 6;

        var (x,y) = rotations[i];

        rindex = i;
        rotation = Quaternion.Euler(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {

        var rot = Quaternion.RotateTowards(cube.rotation, rotation, 180 * Time.deltaTime);
        cube.rotation = rot;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet == null)
            return;

        PickRotation();
    }
}

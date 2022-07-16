using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private List<Attachment> attachments;
    [SerializeField] private List<Consumables> consumables;

    private Transform cube;    
    private Quaternion rotation = Quaternion.Euler(0, 0, 0);
    private int rindex = 2;

    // Start is called before the first frame update
    void Start()
    {
        cube = transform.GetChild(0);

        for (int i = 0; i < attachments.Count; i++)
        {
            var sr = cube.GetChild(i).GetComponent<SpriteRenderer>();
            sr.sprite = attachments[i].crateIcon;
            sr.color = Color.white;
        }

        for (int i = 0; i < consumables.Count; i++)
        {
            var sr = cube.GetChild(attachments.Count + i).GetComponent<SpriteRenderer>();
            sr.sprite = consumables[i].crateIcon;
            sr.color = Color.white;
        }

        PickRotation();
        cube.rotation = rotation;

    }

    private void PickRotation()
    {
        List<(int, int)> rotations = new List<(int, int)> { (0, 180), (180, 270), (0, 0), (90, 90), (90, 180), (90, 0) };

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
        if (collision.gameObject.CompareTag("Player"))
        {
            PickUp();
            Destroy(gameObject);
            return;
        }

        var bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet == null)
            return;

        PickRotation();
    }

    private void PickUp()
    {
        if (rindex < attachments.Count)
        {
            Gun.instance.AddAttachment(attachments[rindex]);
            SoundManager.instance.Play(attachments[rindex].pickUpSound);
            return;
        }
        int index = rindex - attachments.Count;
        consumables[index].Use();
        SoundManager.instance.Play(consumables[index].pickUpSound);
    }
}

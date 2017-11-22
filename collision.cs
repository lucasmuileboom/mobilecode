using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    private win win;
    private bool flipped = false;
    private float rotationX = 0;
    void Start()
    {
        win = GameObject.Find("Game").GetComponent<win>();
    }
    void Update()
    {
        if (flipped && rotationX != 180)
        {
            rotationX += 10;
            transform.rotation = Quaternion.Euler(rotationX, 0, 0);
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !flipped)
        {
            flipped = true;
            win.flippedcellcount();
        }
    }
}

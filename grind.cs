using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grind : MonoBehaviour
{
    [SerializeField]
    private int Xsize;
    [SerializeField]
    private int Zsize;
    [SerializeField]
    private int holls;
    [SerializeField]
    private int speed;
    [SerializeField]
    private GameObject cell;
    [SerializeField]
    private GameObject border;
    private GameObject[,] cells;
    private bool[,] boolcells;
    public int X()
    {
        return Xsize;
    }
    public int Z()
    {
        return Zsize;
    }
    public int holl()
    {
        return holls;
    }
    void Start()
    {
        GetComponent<BoxCollider>().size = new Vector3(Xsize+1,2, Zsize+1);
        transform.position = new Vector3(Xsize / 2, 0, Zsize / 2);
        cells = new GameObject[Xsize, Zsize];
        boolcells = new bool[Xsize, Zsize];
        for (int i = 0; i < Xsize; i++)
        {
            for (int j = 0; j < Zsize; j++)
            {
                boolcells[i, j] = false;
                cells[i, j] = Instantiate(cell, new Vector3(i, 0, j), Quaternion.identity);
                cells[i, j].transform.parent = this.gameObject.transform;
            }
        }
        Instantiate(border, new Vector3((Xsize/2)-0.5f, 0,-1), Quaternion.Euler(0,0,0)).transform.parent = this.gameObject.transform; 
        Instantiate(border, new Vector3(-1, 0, (Zsize/2) - 0.5f), Quaternion.Euler(0, 90, 0)).transform.parent = this.gameObject.transform;
        Instantiate(border, new Vector3((Xsize/2) - 0.5f, 0, Zsize), Quaternion.Euler(0, 180, 0)).transform.parent = this.gameObject.transform;
        Instantiate(border, new Vector3(Xsize, 0, (Zsize / 2) - 0.5f), Quaternion.Euler(0, 270, 0)).transform.parent = this.gameObject.transform;
        for (int i = 0; i < holls; i++)
        {
            checkcells(Random.Range(1, Xsize-1), Random.Range(1, Zsize-1));         
        }
    }
    void checkcells(int x, int z)
    {
        int count = 0;
        if (boolcells[(int)x - 1, (int)z])
        {
            count++;
        }
        if (boolcells[(int)x + 1, (int)z])
        {
            count++;
        }
        if (boolcells[(int)x, (int)z - 1])
        {
            count++;
        }
        if (boolcells[(int)x, (int)z +1])
        {
            count++;
        }
        if (count == 4 || count == 3)
        {
            holls++;
        }
        else
        {
            cells[(int)x, (int)z].gameObject.tag = "Destroy";
            boolcells[(int)x, (int)z] = false;
            foreach (Transform child in cells[(int)x, (int)z].gameObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
    void Update()
    {
        float x = Input.acceleration.x;
        float z = Input.acceleration.z;
        transform.rotation = Quaternion.Euler(-z * speed, 0, -x * speed);

    }
}
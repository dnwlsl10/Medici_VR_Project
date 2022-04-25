using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monitor : MonoBehaviour , BombInterface
{
    public static Monitor instance;
    [SerializeField]
    Color pointColor;
    
    int row;
    int col;
    GameObject[,] points = new GameObject[10, 10];

    [SerializeField]
    List<ArrowBombPattern> patterns;

    ArrowBombPattern pattern;




    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                points[2 * i, 2 * j] = transform.GetChild(i).transform.GetChild(j).gameObject;
            }
        }
        int num = Random.Range(0, patterns.Count);
        pattern = patterns[num];
        row = pattern.startRow;
        col = pattern.startCol;

        points[row,col].GetComponent<MeshRenderer>().material.color = pointColor;
        points[pattern.destinationRow, pattern.destinationCol].GetComponent<MeshRenderer>().material.color = Color.red;

    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            MoveLight(0, -1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveLight(-1, 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveLight(0, 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveLight(1, 0);

        }


    }


    public void MoveLight(int row, int col)
    {
        print("before ::>>  row : " + this.row + "// addedRow :" + row + " // col :" + this.col + "//addedCol :" + col);

        if (this.row + row > 8 || this.row + row < 0 || this.col + col > 8 || this.col + col < 0)
        {
            return;
        }
        points[this.row, this.col].GetComponent<MeshRenderer>().material.color = Color.white;
        this.row += row;
        this.col += col;
        for (int i = 0; i < pattern.bombPositions.Count; i++)
        {
            if (pattern.bombPositions[i].row == this.row && pattern.bombPositions[i].col == this.col)
            {
                Fail();
                return;
            }
        }
        this.row += row;
        this.col += col;

        
        print(points[this.row, this.col].GetComponent<MeshRenderer>());
        points[this.row, this.col].GetComponent<MeshRenderer>().material.color = pointColor;
        print("after ::>>  row : " + this.row + "// addedRow :" + row + " // col :" + this.col + "//addedCol :" + col);
        if (this.row == pattern.destinationRow && this.col == pattern.destinationCol)
        {
            Success();
        }

    }

    public bool Fail()
    {
        print("fail");
        return false;
    }

    public bool Success()
    {
        print("Success");
        return true;
    }

    [Serializable]
    public struct BombPosition
    {
        [SerializeField]
        public int col;
        [SerializeField]
        public int row;
    }

    [Serializable]
    public struct ArrowBombPattern
    {
        public int startRow;
        public int startCol;
        public int destinationRow;
        public int destinationCol;
        [SerializeField]
        public List<BombPosition> bombPositions;
    }
}



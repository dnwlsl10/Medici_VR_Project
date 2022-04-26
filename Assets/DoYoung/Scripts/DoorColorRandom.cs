using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColorRandom : MonoBehaviour
{
    [System.Serializable]
    public class ColorInfo
    {
        public string name;
        public Material mat;
        public Transform bombPosition;
    }
    public MeshRenderer[] doors;
    public ColorInfo[] infos;
    public Outline[] outlines;
    public GameObject bomb;
    int randValue;
    void Start()
    {
        randValue = Random.Range(0, infos.Length);

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].material = infos[randValue].mat;
        }
        bomb.transform.position = infos[randValue].bombPosition.position;
    }

}

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
    int randValue;
    public GameObject bomb;
    void Start()
    {
        randValue = Random.Range(0, infos.Length);

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].material = infos[randValue].mat;
        }
        SetBombPosition(randValue);
    }
    void SetBombPosition(int randValue)
    {
        bomb.transform.position = infos[randValue].bombPosition.position;
    }
}

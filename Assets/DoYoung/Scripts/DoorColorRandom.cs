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
    }
    public MeshRenderer[] doors;
    public ColorInfo[] infos;
    public Outline[] outlines;
    int randValue;
    void Start()
    {
        randValue = Random.Range(0, infos.Length);

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].material = infos[randValue].mat;
        }
    }

    public void OnOutline()
    {
        for (int i = 0; i < outlines.Length; i++)
        {
            outlines[i].OnRayCastEnter();
        }
    }

    public void OffOutline()
    {
        for (int i = 0; i < outlines.Length; i++)
        {
            outlines[i].OnRayCastExit();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCode
{
    Right,
    Left,
    Up,
    Down,
    Back
}
public class BombController : MonoBehaviour
{
    public WireBox wireBox;

    public GameObject[] code;
    public eCode codeIndex;
    private void Start()
    {
        int randomIndex = Random.Range(0, code.Length);
        codeIndex = (eCode)randomIndex;
        code[randomIndex].SetActive(true);
        wireBox.OnMakePattern(this.codeIndex);
    }


}
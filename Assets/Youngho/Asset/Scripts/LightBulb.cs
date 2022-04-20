using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
    [SerializeField]
    GameObject lightSphere;
    [SerializeField]
    GameObject light;
    [SerializeField]
    Material defaultLightSphereMat;
    [SerializeField]
    Color defaultColor;
    [SerializeField]
    Material redLightSphereMat;
    [SerializeField]
    Color redLightColor;
    [SerializeField]
    Material greenLightSphereMat;
    [SerializeField]
    Color greenLightColor;


    // Start is called before the first frame update
    void Start()
    {
        lightSphere.SetActive(false);
        lightSphere.GetComponent<MeshRenderer>().material = defaultLightSphereMat;
        light.GetComponent<Light>().color = defaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            lightSphere.SetActive(true);
            lightSphere.GetComponent<MeshRenderer>().material = redLightSphereMat;
            light.GetComponent<Light>().color = redLightColor;

        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            lightSphere.SetActive(true);
            lightSphere.GetComponent<MeshRenderer>().material = greenLightSphereMat;
            light.GetComponent<Light>().color = greenLightColor;

        }
        if (Input.GetKeyDown(KeyCode.F3))
        {

        }
    }
}

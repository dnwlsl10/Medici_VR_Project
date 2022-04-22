using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public GameObject lightSphere;
    public GameObject light;
    
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

    public void Start()
    {
        lightSphere.SetActive(true);
        lightSphere.GetComponent<MeshRenderer>().material = defaultLightSphereMat;
        light.GetComponent<Light>().color = defaultColor;
        BombManager.OnFailEventLight += OnRedLight;
    }
    public void OnSucess()
    {
        lightSphere.SetActive(true);
        lightSphere.GetComponent<MeshRenderer>().material = greenLightSphereMat;
        light.GetComponent<Light>().color = greenLightColor;
    }

    public void OnFail(System.Action OnRedLight)
    {
        Debug.Log("1");
        lightSphere.SetActive(true);
        lightSphere.GetComponent<MeshRenderer>().material = redLightSphereMat;
        light.GetComponent<Light>().color = redLightColor;
        StartCoroutine(OnActionRedLight(OnRedLight));
    }


    IEnumerator OnActionRedLight(System.Action action)
    {
        int count = 0;
        while(count < 3)
        {
            OnDefultLight();
            yield return new WaitForSeconds(1f);
            OnRedLight();
            yield return new WaitForSeconds(1f);
            count++;
        }
        OnDefultLight();
        yield return new WaitForSeconds(1f);
        action();
    }


    public void OnDefultLight()
    {
        lightSphere.SetActive(true);
        lightSphere.GetComponent<MeshRenderer>().material = defaultLightSphereMat;
        light.GetComponent<Light>().color = defaultColor;
    }

    public void OnRedLight()
    {
        lightSphere.SetActive(true);
        lightSphere.GetComponent<MeshRenderer>().material = redLightSphereMat;
        light.GetComponent<Light>().color = redLightColor;
    }

}

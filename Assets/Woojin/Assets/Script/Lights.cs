using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public GameObject lightSphere;
    public GameObject light;

    public Material battery;
    public Material lastBattery;

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
    bool isSucess;
    bool isFail;
    public void Update()
    {
        if (BombManager.instance.isFail)
        {
            battery.SetColor("_EmissionColor", Color.red);
            lastBattery.SetColor("_EmissionColor", Color.red);
        }
    }

    IEnumerator OnEissionColor()
    {
        while (!isSucess)
        {
            battery.SetColor("_EmissionColor", Color.red);
            yield return new WaitForSeconds(1f);
            battery.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(1f);

            if (BombManager.instance.isFail)
            {
                break;
            }
        }
    }

    IEnumerator OnEissionLastColor()
    {
        while (!BombManager.instance.isSucess)
        {
            lastBattery.SetColor("_EmissionColor", Color.red);
            yield return new WaitForSeconds(1f);
            lastBattery.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(1f);

            if (BombManager.instance.isFail)
            {
                break;
            }
        }
    }

    public void Start()
    {
        light.SetActive(false);
        battery.SetColor("_EmissionColor", Color.black);
        lastBattery.SetColor("_EmissionColor", Color.black);
        lightSphere.SetActive(true);
        lightSphere.GetComponent<MeshRenderer>().material = defaultLightSphereMat;
        light.GetComponent<Light>().color = defaultColor;
        BombManager.OnFailEventLight += OnRedLight;
        StartCoroutine(OnEissionColor());
        StartCoroutine(OnEissionLastColor());
    }
    public void OnSucess()
    {
        light.SetActive(true);
        lightSphere.SetActive(true);
        lightSphere.GetComponent<MeshRenderer>().material = greenLightSphereMat;
        light.GetComponent<Light>().color = greenLightColor;
        isSucess = true;

    }

    public void OnFail(System.Action OnRedLight)
    {
        light.SetActive(true);
        lightSphere.SetActive(true);
        lightSphere.GetComponent<MeshRenderer>().material = redLightSphereMat;
        light.GetComponent<Light>().color = redLightColor;
        StartCoroutine(OnActionRedLight(OnRedLight));
        BombManager.instance.isFail = true;
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
        light.SetActive(true);
        lightSphere.SetActive(true);
        lightSphere.GetComponent<MeshRenderer>().material = defaultLightSphereMat;
        light.GetComponent<Light>().color = defaultColor;
    }

    public void OnRedLight()
    {
        light.SetActive(true);
        lightSphere.SetActive(true);
        lightSphere.GetComponent<MeshRenderer>().material = redLightSphereMat;
        light.GetComponent<Light>().color = redLightColor;
    }

}

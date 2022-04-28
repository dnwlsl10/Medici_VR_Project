using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessScript : MonoBehaviour
{
    public static PostProcessScript instance;
    private void Awake()
    {
        PostProcessScript.instance = this;
    }
    public bool isPlayerDead;
    PostProcessVolume ppv;
    private Vignette vignette;
    private AutoExposure autoExposure;

    // Start is called before the first frame update
    void Start()
    {
        ppv = GetComponent<PostProcessVolume>();
        ppv.profile.TryGetSettings(out autoExposure);
        ppv.profile.TryGetSettings(out vignette);
        autoExposure.active = true;
        vignette.active = true;
    }

    private void Update()
    {
        if (isPlayerDead)
        {
            StartCoroutine(VigenetteOut());
        }
    }
    IEnumerator fadeOut()
    {
        while (autoExposure.minLuminance.value < 9)
        {
            autoExposure.minLuminance.value += 0.1f;
            yield return 0;
        }
    }

    IEnumerator VigenetteOut()
    {
        isPlayerDead = false;
        yield return new WaitForSeconds(2f);
        while (vignette.intensity.value < 1)
        {
            vignette.intensity.value += 0.001f;
            yield return 0;
        }

        StartCoroutine(fadeOut());
    }
}

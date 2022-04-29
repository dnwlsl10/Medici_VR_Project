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
    bool isEndCourutineStart;
    AudioSource playerBreath;

    // Start is called before the first frame update
    void Start()
    {
        playerBreath = GetComponent<AudioSource>();
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
            StartCoroutine(VigenetteOutDead());
        }

        if (BombManager.instance.isGameSuccess && !isEndCourutineStart)
        {
            StartCoroutine(fadeOutSuccess());

            isEndCourutineStart = true;
        }

        if (BombManager.instance.isGameFail)
        {
            Invoke("AudioStop", 15);
        }


    }
    void AudioStop()
    {
        playerBreath.Stop();
    }
    IEnumerator fadeOutDead()
    {
        while (autoExposure.minLuminance.value < 9)
        {
            autoExposure.minLuminance.value += 0.2f;
            yield return 0;
        }

        App.instance.ChangeScene(eSceneType.Title);
    }

    IEnumerator VigenetteOutDead()
    {
        isPlayerDead = false;
        yield return new WaitForSeconds(2f);
        while (vignette.intensity.value < 1)
        {
            vignette.intensity.value += 0.001f;
            yield return 0;
        }

        StartCoroutine(fadeOutDead());
    }

    IEnumerator fadeOutSuccess()
    {
        //:TODO ÇÑ¼û ¼Ò¸®  => 
        SoundManager.instance.PlaySound(Camera.main.transform.position, "Sigh");

        yield return new WaitForSeconds(6);
        SoundManager.instance.PlaySound(Camera.main.transform.position, "FaildSound");


        yield return new WaitForSeconds(1);
        SoundManager.instance.PlaySound(Camera.main.transform.position, "Huk");

        autoExposure.minLuminance.value = 9;
        App.instance.ChangeScene(eSceneType.Title);


    }

}

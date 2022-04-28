using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Lobby : MonoBehaviour
{
    public RightHand rh;
    public System.Action OnGameStart;
    private float count;
    public LoadingController lc;

    void Update()
    {
        if (rh.count == 9)
        {
            StartCoroutine(LoadSceneProcess());
            rh.count = 10;
      
        }
    }
    IEnumerator LoadSceneProcess()
    {
        lc.gameObject.SetActive(true);
        AsyncOperation ao = SceneManager.LoadSceneAsync("InGame");
        ao.allowSceneActivation = false;

        float timer = 0f;
        while (!ao.isDone)
        {
            yield return null;

            if (ao.progress < 0.9f)
            {
                Debug.Log("90га╥н");
                count = ao.progress;
            }
            else
            {

                count += 0.01f;
                if (count >= 1f)
                {
                    yield return new WaitForSeconds(5f);
                    Debug.Log(ao.isDone);
                    ao.allowSceneActivation = true;
                    OnGameStart();
                    yield break;
                }
            }
        }
    }

}
